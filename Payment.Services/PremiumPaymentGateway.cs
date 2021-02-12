using Microsoft.Extensions.Logging;
using Payment.Application.PaymentGateways;
using System.Threading.Tasks;

namespace Payment.Services
{
    internal class PremiumPaymentGateway : IPremiumPaymentGateway
    {
        private readonly ILogger<PremiumPaymentGateway> _logger;

        public PremiumPaymentGateway(ILogger<PremiumPaymentGateway> logger)
        {
            _logger = logger;
        }
        public Task<bool> CaptureAsync(decimal amount, string cardNumber, string securityCode)
        {
            _logger.LogInformation($"Payment captured using {nameof(PremiumPaymentGateway)}");
            return Task.FromResult(true);
        }
    }
}
