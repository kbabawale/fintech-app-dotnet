using System;
using FluentValidation;
using Fintech_App.Util;
using System.Text.Json;

namespace Fintech_App.Middlewares
{
    public class Errorhandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Errorhandler> _logger;

        public Errorhandler(RequestDelegate next, ILogger<Errorhandler> logger)
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
                _logger.LogInformation("Error occurred");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = ApiResponse<string>.FailureResponse(
                    ex.Message,
                    "An unexpected error occurred.");

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
