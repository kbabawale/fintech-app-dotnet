using System;
using FluentValidation;
using Fintech_App.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

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
            catch (DbUpdateConcurrencyException ex)
            {
                await HandleException(context, ex, "The data was modified by another user. Please reload.");
            }
            catch (Exceptions.ValidationException ex)
            {
                await HandleException(context, ex, ex.Message, true);
            }
            catch (DbUpdateException ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;

                if (ex.InnerException is SqlException sqlEx)
                {
                    switch (sqlEx.Number)
                    {
                        case 2627: // Unique constraint error
                        case 2601: // Duplicated key row error
                            await HandleException(context, ex, "A duplicate record was found", true, StatusCodes.Status409Conflict);
                            break;
                        case 547: // Foreign key violation
                            await HandleException(context, ex, message);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex, string? message = null, bool showMessage = false, int status = StatusCodes.Status400BadRequest)
        {
            var msg = showMessage && message is not null ? message : !showMessage ? "An unexpected error occurred" : ex.Message;
            _logger.LogInformation(ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;

            var response = ApiResponse<string>.FailureResponse(
                msg,
                "An unexpected error occurred.");

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
