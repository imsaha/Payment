using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Enumerations
{
    public enum PaymentState
    {
        Pending = 0,
        Processed = 1,
        Failed = 2
    }
}
