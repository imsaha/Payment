using Payment.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Models
{
    public class PaymentRequestState : ModelBase<long>
    {
        public long RequestId { get; set; }
        public PaymentRequest Request { get; set; }
        public PaymentState State { get; set; }
        public DateTimeOffset At { get; set; }
    }
}
