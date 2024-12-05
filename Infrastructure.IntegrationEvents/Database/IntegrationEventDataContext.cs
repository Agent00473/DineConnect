using Infrastructure.IntegrationEvents.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IntegrationEvents.Database
{
    public sealed class IntegrationEventDataContext: DbContext
    {
        private readonly string _connectionString;

        public IntegrationEventDataContext(DbContextOptions<IntegrationEventDataContext> options) : base(options)
        {
            _connectionString = string.Empty;
        }
        public IntegrationEventDataContext(string connectionString)
        : this(new DbContextOptionsBuilder<IntegrationEventDataContext>().UseNpgsql(connectionString) .Options)
        {
            _connectionString = connectionString;
        }
        public DbSet<IntegrationEventDetail> EventDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IntegrationEventDetail>(builder =>
            {
                builder.ToTable("IntegrationEventData");
                builder.HasKey(e => e.EventId);
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }
    }
}
