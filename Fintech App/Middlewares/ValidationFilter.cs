using Fintech_App.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Fintech_App.Middlewares
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = string.Join(
                    "; ",
                    context.ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage)
                );

                var response = ApiResponse<object>.FailureResponse(errors, "Validation failed");

                context.Result = new BadRequestObjectResult(response);
                return;
            }

            await next();
        }
    }
}