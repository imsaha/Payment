using FluentValidation;
using MediatR;
using Payment.Application.PaymentGateways;
using Payment.Domain.Enumerations;
using Payment.Domain.Models;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Application.PaymentProcess.Commands
{
    public class ProcessPaymentCommand : IRequest<Result<long>>
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }

        public class Validator : AbstractValidator<ProcessPaymentCommand>
        {
            public Validator()
            {

                var cardRegex = @"^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$";

                RuleFor(x => x.CardNumber)
                    .NotEmpty().WithMessage("Card number can not be empty.")
                    .Matches(cardRegex).WithMessage("Card number is invalid.");

                RuleFor(x => x.CardHolderName)
                    .NotEmpty().WithMessage("Car holder name can not be empty.");

                RuleFor(x => x.ExpirationDate)
                    .NotEmpty()
                    .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Card is expired.");

                RuleFor(x => x.Amount)
                    .GreaterThan(0).WithMessage("Amount must have a valid number.");

                When(x => !string.IsNullOrEmpty(x.SecurityCode), () =>
                {
                    RuleFor(x => x.SecurityCode)
                    .Must(c => int.TryParse(c, out int code)).WithMessage("Invalid security code.")
                    .Length(3).WithMessage("Invalid security code.");
                });
            }
        }


        public class Handler : IRequestHandler<ProcessPaymentCommand, Result<long>>
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly IDataContext _dataContext;
            private readonly ICheapPaymentGateway _cheapPaymentGateway;
            private readonly IExpensivePaymentGateway _expensivePaymentGateway;
            private readonly IPremiumPaymentGateway _premiumPaymentGateway;

            public Handler(
                ICurrentUserService currentUserService,
                IDataContext dataContext,
                ICheapPaymentGateway cheapPaymentGateway,
                IExpensivePaymentGateway expensivePaymentGateway,
                IPremiumPaymentGateway premiumPaymentGateway)
            {
                _currentUserService = currentUserService;
                _dataContext = dataContext;
                _cheapPaymentGateway = cheapPaymentGateway;
                _expensivePaymentGateway = expensivePaymentGateway;
                _premiumPaymentGateway = premiumPaymentGateway;
            }
            public async Task<Result<long>> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
            {
                var isPaymentCaptured = await capturePaymentAsync(request);
                var paymentState = isPaymentCaptured ? PaymentState.Processed : PaymentState.Failed;
                var payment = new PaymentRequest()
                {
                    Amount = request.Amount,
                    Card = new Domain.ValueObjects.CreditCard()
                    {
                        CardHolderName = request.CardHolderName,
                        ExpirationDate = request.ExpirationDate,
                        Number = request.CardNumber,
                        SecurityCode = request.SecurityCode
                    },
                    CurrentState = paymentState,
                    IsDeleted = false,
                };

                payment.States.Add(new PaymentRequestState()
                {
                    Request = payment,
                    State = paymentState,
                    At = DateTimeOffset.Now,
                });

                _dataContext.Payments.Add(payment);
                await _dataContext.SaveChangesAsync(cancellationToken);

                return Result.Ok(payment.Id);
            }

            private async Task<bool> capturePaymentAsync(ProcessPaymentCommand param)
            {
                if (param.Amount <= 20)
                {
                    return await _cheapPaymentGateway.CaptureAsync(param.Amount, param.CardNumber, param.SecurityCode);
                }
                else if (param.Amount <= 500)
                {
                    var maxRetryCount = 2;
                    var retryLeft = maxRetryCount;
                    var isCapturedSuccess = false;

                    while (retryLeft > 0)
                    {
                        try
                        {
                            if (retryLeft == maxRetryCount)
                            {
                                isCapturedSuccess = await _expensivePaymentGateway.CaptureAsync(param.Amount, param.CardNumber, param.SecurityCode);
                            }
                            else
                            {
                                isCapturedSuccess = await _cheapPaymentGateway.CaptureAsync(param.Amount, param.CardNumber, param.SecurityCode);
                            }
                            retryLeft = 0;
                        }
                        catch (Exception)
                        {
                            retryLeft--;
                            if (retryLeft == 0)
                                throw;
                        }
                    }

                    return isCapturedSuccess;
                }
                else
                {
                    var maxRetryCount = 3;
                    var retryLeft = maxRetryCount;
                    var isCapturedSuccess = false;

                    while (retryLeft > 0)
                    {
                        try
                        {
                            isCapturedSuccess = await _premiumPaymentGateway.CaptureAsync(param.Amount, param.CardNumber, param.SecurityCode);
                            retryLeft = 0;
                        }
                        catch (Exception)
                        {
                            retryLeft--;

                            if (retryLeft == 0)
                                throw;
                        }
                    }

                    return isCapturedSuccess;
                }
            }
        }
    }
}
