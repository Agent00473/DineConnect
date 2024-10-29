using DineConnect.PaymentManagementService.Domain.Invoice;
using DineConnect.PaymentManagementService.Domain.Invoice.ValueObjects;
using DineConnect.PaymentManagementService.Domain.Payment.Entities;
using DineConnect.PaymentManagementService.Domain.Payment.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Transactions;

namespace DineConnect.PaymentManagementService.Infrastructure.DataAccess.Configurations
{
    public class RefundConfigurator : IEntityTypeConfiguration<Refund>
    {

        private void ConfigureRefundTable(EntityTypeBuilder<Refund> builder)
        {
            builder.ToTable("Refunds").HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .ValueGeneratedNever()
                .HasColumnType("uuid")
                .HasConversion(
                    id => id.Id,
                    value => RefundId.Create(value));
            builder.Property(r => r.RefundAmount)
                          .HasColumnName("RefundAmount")
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.Property(r => r.RefundDate)
                          .HasColumnName("RefundDate")
                          .IsRequired();

            builder.Property(r => r.Reason)
                          .HasColumnName("Reason")
                          .HasColumnType("int")
                          .IsRequired();

            builder.Property(r => r.Status)
                          .HasColumnName("Status")
                           .HasColumnType("int")
                          .IsRequired();

            builder.Property(r => r.InvoiceId)
                .HasColumnType("uuid")
                .HasColumnName("InvoiceId")
                .HasConversion(
                    id => id.IdValue,
                    value => InvoiceId.Create(value));

            builder.Property(r => r.TransactionId)
                .HasColumnType("uuid")
                .HasColumnName("TransactionId")
                .HasConversion(
                    id => id.Id,
                    value => TransactionId.Create(value));

            builder.HasOne<PaymentTransaction>()
                    .WithOne()
                    .HasForeignKey<Refund>(r => r.TransactionId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Invoice>()
                    .WithOne()
                    .HasForeignKey<Refund>(r => r.InvoiceId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Metadata
                .FindProperty(nameof(Refund.InvoiceId))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
                .FindProperty(nameof(Refund.TransactionId))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            ConfigureRefundTable(builder);
        }
    }
}
