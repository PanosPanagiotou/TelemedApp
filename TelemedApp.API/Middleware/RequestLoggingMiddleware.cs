namespace TelemedApp.API.Middleware
{
    public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<RequestLoggingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Items["X-Correlation-ID"]?.ToString() ?? "-";

            _logger.LogInformation(
                "Incoming Request {method} {path} | CorrelationId: {correlationId}",
                context.Request.Method,
                context.Request.Path,
                correlationId
            );

            await _next(context);

            _logger.LogInformation(
                "Outgoing Response {statusCode} | CorrelationId: {correlationId}",
                context.Response.StatusCode,
                correlationId
            );
        }
    }
}
/*
 * What this does:
 * Logs every request
 * Logs every response
 * Includes correlation ID automatically
 * Helps us trace issues across layers
 */