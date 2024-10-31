using DineConnect.OrderManagementService.Domain.Orders.Entities;
using DineConnect.OrderManagementService.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using DineConnect.OrderManagementService.Domain.Interfaces;
using DineConnect.OrderManagementService.Domain.Customers;
using DineConnect.OrderManagementService.Infrastructure.DataAccess.Configurations;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess
{
    public sealed class DineOutOrderDbContext: DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> MenuItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }

        public DineOutOrderDbContext(DbContextOptions<DineOutOrderDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ////
            ///Another Option
            ///
            //modelBuilder.ApplyConfiguration(new DeliveryAddressConfigurator());
            //modelBuilder.ApplyConfiguration(new CustomerConfiguration()); 
            //modelBuilder.ApplyConfiguration(new RestaurantConfigurations());
            //modelBuilder.ApplyConfiguration(new OrderConfigurator());
            
            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(DineOutOrderDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

}
