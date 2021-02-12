using System.Threading.Tasks;

namespace Payment.Application.PaymentGateways
{
    public interface IPremiumPaymentGateway
    {
        Task<bool> CaptureAsync(decimal amount, string cardNumber, string securityCode);
    }
}
