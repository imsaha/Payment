using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payment.Api.RequestDto;
using Payment.Application.PaymentProcess.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Api.Controllers.v1
{
    public class PaymentController : V1ControllerBase
    {
        public PaymentController(IMediator mediator) : base(mediator)
        {
        }

        [Route("ProcessPayment"), HttpPost]
        public async Task<IActionResult> ProcessPaymentAsync(PaymentProcessRequest request)
        {
            return Ok(await _mediator.Send(new ProcessPaymentCommand()
            {
                Amount = request.Amount,
                CardHolderName = request.CardHolderName,
                CardNumber = request.CardNumber,
                ExpirationDate = request.ExpirationDate,
                SecurityCode = request.SecurityCode
            }));
        }
    }
}
