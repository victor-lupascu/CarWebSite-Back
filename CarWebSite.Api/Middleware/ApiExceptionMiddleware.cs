using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarWebSite.Api.Middleware
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;

        public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    throw;
                }

                await WriteErrorAsync(context, ex);
            }
        }

        private async Task WriteErrorAsync(HttpContext context, Exception ex)
        {
            var statusCode = GetStatusCode(ex);
            _logger.LogError(ex, "Unhandled API exception returned {StatusCode}", statusCode);

            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = GetTitle(statusCode),
                Detail = GetDetail(ex, statusCode),
                Instance = context.Request.Path
            };

            problem.Extensions["traceId"] = context.TraceIdentifier;

            await context.Response.WriteAsJsonAsync(problem);
        }

        private static int GetStatusCode(Exception ex)
        {
            if (ex is DbUpdateException) return (int)HttpStatusCode.BadRequest;
            if (IsDatabaseUnavailable(ex)) return (int)HttpStatusCode.ServiceUnavailable;
            if (ex is BadHttpRequestException || ex is InvalidOperationException)
            {
                return (int)HttpStatusCode.BadRequest;
            }

            return (int)HttpStatusCode.InternalServerError;
        }

        private static bool IsDatabaseUnavailable(Exception ex)
        {
            for (var current = ex; current != null; current = current.InnerException)
            {
                if (current.GetType().Name == "SqlException") return true;
            }

            return false;
        }

        private static string GetTitle(int statusCode) =>
            statusCode switch
            {
                StatusCodes.Status400BadRequest => "Invalid request",
                StatusCodes.Status503ServiceUnavailable => "Service unavailable",
                _ => "Server error"
            };

        private static string GetDetail(Exception ex, int statusCode) =>
            statusCode switch
            {
                StatusCodes.Status400BadRequest => "The request could not be processed. Please check the submitted data.",
                StatusCodes.Status503ServiceUnavailable => "The database is unavailable. Please start SQL Server and try again.",
                _ => "An unexpected error occurred while processing the request."
            };
    }
}
