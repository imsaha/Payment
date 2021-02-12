using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Payment.Application;
using Payment.Application.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Api.CustomFilters
{
    public class CustomValidationResponseActionFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                List<ValidationFailure> failures = new List<ValidationFailure>();
                foreach (var item in context.ModelState)
                {
                    foreach (var error in item.Value.Errors)
                    {
                        failures.Add(new ValidationFailure(item.Key, error.ErrorMessage));
                    }
                }

                var exception = new AppValidationException(failures);
                var responseObj = Result.Failed(exception.Message, exception.Failures);

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                return Task.CompletedTask;
            }
            else
            {
                return next();
            }
        }
    }
}
