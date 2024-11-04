using DineConnect.PaymentManagementService.Domain.Invoice;
using DineConnect.PaymentManagementService.Domain.Invoice.ValueObjects;
using DineConnect.PaymentManagementService.Domain.Payment;
using DineConnect.PaymentManagementService.Domain.Payment.Entities;
using DineConnect.PaymentManagementService.Domain.Payment.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DineConnect.PaymentManagementService.Infrastructure.DataAccess.Configurations
{
    internal class PaymentConfigurator : IEntityTypeConfiguration<Payment>
    {
        private void ConfigurePaymentTable(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasColumnType("uuid")
            .HasConversion(
              id => id.IdValue,
              value => PaymentId.Create(value));

            builder.Property(p => p.Amount)
                   .HasColumnName("Amount")
                   .HasColumnType("decimal(5,2)");

            builder.Property(p => p.PaymentDate)
                   .HasColumnName("PaymentDate");

            builder.Property(p => p.Status)
                   .HasColumnName("Status");

            builder.OwnsOne(p => p.Method);

            builder.Property(p => p.InvoiceId)
                .IsRequired()
                .HasConversion(
                    id => id.IdValue,
                    value => InvoiceId.Create(value))
                .HasColumnName("InvoiceId");


            builder.Property(p => p.CustomerId)
                .IsRequired()
                .HasColumnName("CustomerId")
                .HasConversion(
                    id => id.Id,
                    value => CustomerId.Create(value));

            builder.HasOne<Invoice>()
                    .WithOne()
                    .HasForeignKey<Payment>(p => p.InvoiceId) // Specify InvoiceId as the foreign key in Payment
                    .OnDelete(DeleteBehavior.Restrict); // Set delete behavior

            builder.HasMany<PaymentTransaction>(p => p.Transactions) // Use Transactions property
            .WithOne() // Assuming PaymentTransaction does not navigate back to Payment
            .HasForeignKey("PaymentId") // Set PaymentId as the foreign key in PaymentTransaction
            .OnDelete(DeleteBehavior.Cascade); // Set delete behavior as needed

            builder.Metadata
            .FindNavigation(nameof(Payment.Transactions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        }

        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            ConfigurePaymentTable(builder);
        }

    }
}
