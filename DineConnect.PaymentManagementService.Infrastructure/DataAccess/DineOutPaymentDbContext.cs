using DineConnect.PaymentManagementService.Domain.Interfaces;
using DineConnect.PaymentManagementService.Domain.Invoice;
using DineConnect.PaymentManagementService.Domain.Payment;
using DineConnect.PaymentManagementService.Domain.Payment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DineConnect.PaymentManagementService.Infrastructure.DataAccess
{
    public sealed class DineOutPaymentDbContext: DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Refund> Refunds { get; set; }

        public DineOutPaymentDbContext(DbContextOptions<DineOutPaymentDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(DineOutPaymentDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
