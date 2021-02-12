using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application
{
    public interface ICurrentUserService
    {
        public long? UserId { get; }
    }
}
