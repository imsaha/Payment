using Payment.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Api.Helpers
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService()
        {

        }
        public long? UserId { get; }
    }
}
