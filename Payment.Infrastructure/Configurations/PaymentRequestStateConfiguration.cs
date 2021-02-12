using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain.Models;

namespace Payment.Infrastructure.Configurations
{
    public class PaymentRequestStateConfiguration : IEntityTypeConfiguration<PaymentRequestState>
    {
        public void Configure(EntityTypeBuilder<PaymentRequestState> builder)
        {
            builder.ToTable("tblPaymentStates");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.RequestId, x.State, x.At }).IsUnique();

            builder.Property(x => x.State).HasConversion<string>();

            builder.HasOne(x => x.Request).WithMany(x => x.States).HasForeignKey(x => x.RequestId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
