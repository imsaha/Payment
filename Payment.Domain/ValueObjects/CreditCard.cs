using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.ValueObjects
{
    public class CreditCard : ValueObject
    {
        public string Number { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Number;
            yield return CardHolderName;
            yield return ExpirationDate;
            yield return SecurityCode;
        }
    }
}
