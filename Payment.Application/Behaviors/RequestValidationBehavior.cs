using FluentValidation;
using MediatR;
using Payment.Application.Exceptions;
using Payment.Application.PaymentProcess.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Application.Behaviors
{
    internal class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;
        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                validateRequest(request);
                return await next();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Request", request);
                throw ex;
            }
        }

        private void validateRequest(TRequest request)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null).ToList();

            if (failures.Count != 0)
                throw new AppValidationException(failures);
        }
    }
}
