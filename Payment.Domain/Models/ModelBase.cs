using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Models
{
    public abstract class ModelBase<TId> where TId : IEquatable<TId>
    {
        public TId Id { get; set; }
    }
}
