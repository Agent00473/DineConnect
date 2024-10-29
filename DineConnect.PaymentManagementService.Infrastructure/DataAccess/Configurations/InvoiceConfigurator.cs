using DineConnect.PaymentManagementService.Domain.Invoice;
using DineConnect.PaymentManagementService.Domain.Invoice.ValueObjects;
using DineConnect.PaymentManagementService.Domain.Payment.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DineConnect.PaymentManagementService.Infrastructure.DataAccess.Configurations
{
    internal class InvoiceConfigurator : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices").HasKey(inv => inv.Id);
            builder.Property(inv => inv.Id)
                .ValueGeneratedNever()
                .HasColumnType("uuid")
                .HasConversion(
                    id => id.IdValue,
                    value => InvoiceId.Create(value));
            builder.Property(inv => inv.InvoiceDate)
                .HasColumnName("InvoiceDate");

            builder.Property(inv => inv.TotalAmount)
             .HasColumnType("decimal(5,2)")
             .IsRequired();

            builder.Property(inv => inv.DueDate)
                             .HasColumnName("DueDate");
            builder.Property(inv => inv.Status)
                             .HasColumnName("Status");
            
            builder.Property(i => i.PaymentId)
                 .HasColumnName("PaymentId")
                .HasColumnType("uuid")
                .HasConversion(
                    id => id.IdValue,
                    value => PaymentId.Create(value));
        }
}
}
