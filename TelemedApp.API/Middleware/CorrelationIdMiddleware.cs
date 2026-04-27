using System.Diagnostics;

namespace TelemedApp.API.Middleware
{
    public class CorrelationIdMiddleware(RequestDelegate next)
    {
        private const string HeaderName = "X-Correlation-ID";
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            // Try to read incoming correlation ID
            if (!context.Request.Headers.TryGetValue(HeaderName, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }

            // Set correlation ID in HttpContext
            context.Items[HeaderName] = correlationId;

            // Add to response headers
            context.Response.OnStarting(() =>
            {
                context.Response.Headers[HeaderName] = correlationId;
                return Task.CompletedTask;
            });

            // Add to logging scope
            using (context.RequestServices.GetRequiredService<ILoggerFactory>()
                       .CreateLogger("Request")
                       .BeginScope(new Dictionary<string, object> { ["CorrelationId"] = correlationId }))
            {
                await _next(context);
            }
        }
    }
}

/* 
 * What this does:
 * Accepts incoming correlation IDs (from frontend, mobile app, etc.)
 * Generates one if missing
 * Adds it to the response
 * Adds it to the logging scope
 * Makes it available in all logs
 */