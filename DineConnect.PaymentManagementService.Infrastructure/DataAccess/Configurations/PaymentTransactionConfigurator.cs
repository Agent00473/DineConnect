using DineConnect.PaymentManagementService.Domain.Payment.Entities;
using DineConnect.PaymentManagementService.Domain.Payment.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DineConnect.PaymentManagementService.Infrastructure.DataAccess.Configurations
{
    public class PaymentTransactionConfigurator : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            ConfigurePaymentTransactionTable(builder);
        }

        private void ConfigurePaymentTransactionTable(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.ToTable("PaymentTransactions").HasKey(t => t.Id);
            builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasColumnType("uuid")
            .HasConversion(
              id => id.Id,
              value => TransactionId.Create(value));

            builder.Property(p => p.TransactionAmount)
                 .HasColumnName("TransactionAmount")
                 .HasColumnType("decimal(5,2)");

            builder.Property(p => p.TransactionDate)
                 .HasColumnName("TransactionDate");

            builder.Property(p => p.Status)
                 .HasColumnName("Status")
                 .HasColumnType("int");

            builder.Property(p => p.Gateway)
                 .HasColumnName("Gateway")
                 .HasColumnType("int");

            builder.Property(p => p.Category)
                 .HasColumnName("Category")
                 .HasColumnType("int");
        }
    }
}
