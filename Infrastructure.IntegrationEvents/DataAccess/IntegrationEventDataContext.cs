using Infrastructure.IntegrationEvents.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Infrastructure.IntegrationEvents.DataAccess
{
    /// <summary>
    /// TO BE USED ONLY FOR DB MIGRATION
    /// </summary>
    public sealed class IntegrationEventDataContext : DbContext
    {
        private readonly string _connectionString;

        public IntegrationEventDataContext(DbContextOptions<IntegrationEventDataContext> options) : base(options)
        {
            _connectionString = Database.GetConnectionString();
        }

        public IntegrationEventDataContext(DbContextOptions<IntegrationEventDataContext> options, string connectionString) : base(options)
        {
            _connectionString = connectionString;
        }
        public IntegrationEventDataContext(IConnection connection)
    : base()
        {
        }

        public IntegrationEventDataContext(string connectionString)
        : this(new DbContextOptionsBuilder<IntegrationEventDataContext>().UseNpgsql(connectionString).Options, connectionString)
        {
        }
        public DbSet<IntegrationEventDetail> EventDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(IntegrationEventDataContext).Assembly);
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
