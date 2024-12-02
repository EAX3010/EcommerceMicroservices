using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Shared.Exceptions.Handler
{
    internal readonly record struct ExceptionDetails(string Message, string Name, int StatusCode);

    public sealed class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        private const string TraceIdKey = "traceId";
        private const string ValidationErrorsKey = "validationErrors";

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(httpContext);
            ArgumentNullException.ThrowIfNull(exception);

            LogException(exception);

            ExceptionDetails details = GetExceptionDetails(exception, httpContext);
            ProblemDetails problemDetails = CreateProblemDetails(details, httpContext, exception);

            httpContext.Response.StatusCode = details.StatusCode;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(
                problemDetails, cancellationToken);

            return true;
        }

        private void LogException(Exception exception)
        {
            logger.LogError(
                "Error Message: {ExceptionMessage}, Time: {Time}, StackTrace: {StackTrace}",
                exception.Message,
                DateTime.UtcNow,
                exception.StackTrace);
        }

        private static ExceptionDetails GetExceptionDetails(Exception exception, HttpContext httpContext)
        {
            return exception switch
            {
                InternalServerException => new ExceptionDetails(
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError),

                CustomValidationException or BadRequestException => new ExceptionDetails(
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),

                NotFoundException => new ExceptionDetails(
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound),

                NpgsqlException => new ExceptionDetails(
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError),

                _ => new ExceptionDetails(
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError)
            };
        }

        private static ProblemDetails CreateProblemDetails(
            ExceptionDetails details,
            HttpContext httpContext,
            Exception exception)
        {
            ProblemDetails problemDetails = new ProblemDetails
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