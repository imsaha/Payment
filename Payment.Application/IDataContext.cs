using Microsoft.EntityFrameworkCore;
using Payment.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Application
{
    public interface IDataContext
    {
        public DbSet<PaymentRequest> Payments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
