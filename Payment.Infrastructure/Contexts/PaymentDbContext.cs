using Microsoft.EntityFrameworkCore;
using Payment.Application;
using Payment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Infrastructure.Contexts
{
    internal class PaymentDbContext : DbContext, IDataContext
    {
        private readonly ICurrentUserService _currentUserService;

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }


        public DbSet<PaymentRequest> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
