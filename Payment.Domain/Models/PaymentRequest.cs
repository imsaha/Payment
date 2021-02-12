using Payment.Domain.Enumerations;
using Payment.Domain.ValueObjects;
using System.Collections.Generic;

namespace Payment.Domain.Models
{
    public class PaymentRequest : ModelBase<long>
    {
        public PaymentRequest()
        {
            States = new HashSet<PaymentRequestState>();
        }
        public CreditCard Card { get; set; }
        public decimal Amount { get; set; }
        public bool IsDeleted { get; set; }
        public PaymentState CurrentState { get; set; }
        public ICollection<PaymentRequestState> States { get; }
    }
}
