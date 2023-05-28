using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

// Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore error pages.
// Use this middleware to detect and diagnose errors with Entity Framework Core migrations.

namespace UniversityManagementSystem.Models
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception error occurred.");

                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("An unexpected error occurred.");

                // You can also customize the error response based on the exception type or handle specific exceptions separately.
                // For example, you can return different HTTP status codes for different exceptions.
            }
        }
    }
}
