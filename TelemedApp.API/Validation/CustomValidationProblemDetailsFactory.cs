using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TelemedApp.API.Validation
{
    public class CustomValidationProblemDetailsFactory : ProblemDetailsFactory
    {
        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            return new ProblemDetails
            {
                Status = statusCode ?? 400,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance
            };
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext,
            ModelStateDictionary modelStateDictionary,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            var errors = modelStateDictionary
                .Where(kvp => kvp.Value?.Errors.Count > 0)
                .SelectMany(kvp => kvp.Value!.Errors.Select(e => new
                {
                    field = kvp.Key,
                    message = e.ErrorMessage
                }))
                .ToList();

            var problem = new ValidationProblemDetails
            {
                Status = statusCode ?? 400,
                Title = "ValidationError",
                Type = "ValidationError",
                Detail = detail,
                Instance = instance
            };

            problem.Extensions["errors"] = errors;

            return problem;
        }
    }
}