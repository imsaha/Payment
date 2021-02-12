using Microsoft.EntityFrameworkCore;
using Payment.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Infrastructure.DesignTime
{
    internal class PaymentDbContextFactory : DesignTimeDbContextFactoryBase<PaymentDbContext>
    {
        public PaymentDbContextFactory() : base("DefaultConnection")
        {
        }
        protected override PaymentDbContext CreateNewInstance(DbContextOptions<PaymentDbContext> options)
        {
            return new PaymentDbContext(options);
        }
    }
}
