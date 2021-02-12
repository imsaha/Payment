using Microsoft.Extensions.Logging;
using Payment.Application.PaymentGateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Services
{
    internal class CheapPaymentGateway : ICheapPaymentGateway
    {
        private readonly ILogger<CheapPaymentGateway> _logger;

        public CheapPaymentGateway(ILogger<CheapPaymentGateway> logger)
        {
            _logger = logger;
        }

        public Task<bool> CaptureAsync(decimal amount, string cardNumber, string securityCode)
        {
            _logger.LogInformation($"Payment captured using {nameof(CheapPaymentGateway)}");
            return Task.FromResult(true);
        }
    }
}
