using System.Threading.Tasks;

namespace Payment.Application.PaymentGateways
{
    public interface ICheapPaymentGateway
    {
        Task<bool> CaptureAsync(decimal amount, string cardNumber, string securityCode);
    }
}
