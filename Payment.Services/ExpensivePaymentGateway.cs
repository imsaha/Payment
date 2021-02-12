using Microsoft.Extensions.Logging;
using Payment.Application.PaymentGateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Services
{
    internal class ExpensivePaymentGateway : IExpensivePaymentGateway
    {
        private readonly ILogger<ExpensivePaymentGateway> _logger;

        public ExpensivePaymentGateway(ILogger<ExpensivePaymentGateway> logger)
        {
            _logger = logger;
        }

        public Task<bool> CaptureAsync(decimal amount, string cardNumber, string securityCode)
        {
            _logger.LogInformation($"Payment captured using {nameof(ExpensivePaymentGateway)}");
            return Task.FromResult(true);
        }
    }
}
