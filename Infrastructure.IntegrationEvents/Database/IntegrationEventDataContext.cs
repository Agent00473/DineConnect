using Infrastructure.IntegrationEvents.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IntegrationEvents.Database
{
    public sealed class IntegrationEventDataContext: DbContext
    {
        private readonly IConfiguration _configuration;

        public IntegrationEventDataContext(DbContextOptions<IntegrationEventDataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
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
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
