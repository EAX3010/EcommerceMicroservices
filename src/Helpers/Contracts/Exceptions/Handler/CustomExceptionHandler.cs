using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Shared.Exceptions.Handler
{
    internal readonly record struct ExceptionDetails(string Message, string Name, int StatusCode);

    public sealed class CustomExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger;
        private const string TraceIdKey = "traceId";
        private const string ValidationErrorsKey = "validationErrors";

        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(httpContext);
            ArgumentNullException.ThrowIfNull(exception);

            LogException(exception);

            var details = GetExceptionDetails(exception, httpContext);
            var problemDetails = CreateProblemDetails(details, httpContext, exception);

            httpContext.Response.StatusCode = details.StatusCode;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(
                problemDetails,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase },
                cancellationToken);

            return true;
        }

        private void LogException(Exception exception)
        {
            _logger.LogError(
                "Error Message: {ExceptionMessage}, Time: {Time}, StackTrace: {StackTrace}",
                exception.Message,
                DateTime.UtcNow,
                exception.StackTrace);
        }

        private static ExceptionDetails GetExceptionDetails(Exception exception, HttpContext httpContext) =>
            exception switch
            {
                InternalServerException => new(
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError),

                CustomValidationException or BadRequestException => new(
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),

                NotFoundException => new(
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound),

                _ => new(
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError)
            };

        private static ProblemDetails CreateProblemDetails(
            ExceptionDetails details,
            HttpContext httpContext,
            Exception exception)
        {
            var problemDetails = new ProblemDetails
            {
                Title = details.Name,
                Detail = details.Message,
                Status = details.StatusCode,
                Instance = httpContext.Request.Path,
                Type = $"https://httpstatuses.com/{details.StatusCode}"
            };

            problemDetails.Extensions[TraceIdKey] = httpContext.TraceIdentifier;

            if (exception is CustomValidationException validationException)
            {
                problemDetails.Extensions[ValidationErrorsKey] = validationException.Errors;
            }

            return problemDetails;
        }
    }
}