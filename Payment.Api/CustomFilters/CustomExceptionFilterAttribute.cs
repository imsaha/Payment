using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Payment.Application;
using Payment.Application.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Payment.Api.CustomFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            handleExceptions(context);
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }

        private void handleExceptions(ExceptionContext context)
        {
            var exception = context.Exception;
            const string ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";
            string environment = Environment.GetEnvironmentVariable(ASPNETCORE_ENVIRONMENT);

            context.HttpContext.Response.ContentType = "application/json";

            if (exception is AppException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                if (context.Exception is AppAccessDeniedException)
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Result = new JsonResult(Result.Failed(((AppAccessDeniedException)context.Exception).Message));
                    return;
                }
                else if (context.Exception is AppValidationException)
                {
                    context.Result = new JsonResult(Result.Failed(((AppValidationException)context.Exception).Message, ((AppValidationException)context.Exception).Failures));

                    return;
                }
                else
                {
                    string errorMessage = "Something went wrong.";
                    if (environment == "Development")
                        errorMessage = (context.Exception as AppException).Message;
                    context.Result = new JsonResult(Result.Failed(errorMessage));
                    return;
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                string message = "Unknown error occurred.";
                if (environment == "Development")
                    message = (context.Exception).Message;
                context.Result = new JsonResult(Result.Failed(message));
                return;
            }
        }
    }

}
