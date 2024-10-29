using DineConnect.PaymentManagementService.Domain.Interfaces;
using DineConnect.PaymentManagementService.Domain.Invoice;
using DineConnect.PaymentManagementService.Domain.Payment;
using DineConnect.PaymentManagementService.Domain.Payment.Entities;
using Microsoft.EntityFrameworkCore;


namespace DineConnect.PaymentManagementService.Infrastructure.DataAccess
{
    public sealed class DineOutPaymentDbContext: DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Refund> Refunds { get; set; }

        public DineOutPaymentDbContext(DbContextOptions<DineOutPaymentDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(DineOutPaymentDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
