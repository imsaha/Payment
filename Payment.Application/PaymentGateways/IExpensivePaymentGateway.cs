using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.PaymentGateways
{

    public interface IExpensivePaymentGateway
    {
        Task<bool> CaptureAsync(decimal amount, string cardNumber, string securityCode);
    }
}
