using DineConnect.OrderManagementService.Domain.Customers;
using DineConnect.OrderManagementService.Domain.Customers.ValueObjects;
using DineConnect.OrderManagementService.Domain.Orders;
using DineConnect.OrderManagementService.Domain.Orders.ValueObjects;
using DineConnect.OrderManagementService.Domain.Restaurant.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Configurations
{
    internal class OrderConfigurator : IEntityTypeConfiguration<Order>
    {
        #region Private Methods
        private void ConfigureOrderTable(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders")
                .HasKey(o => o.Id); 
            builder.Property(o => o.Id)
                .ValueGeneratedNever()
                .HasColumnType("uuid")
                .HasConversion(
                  id => id.IdValue,
                  value => OrderId.Create(value));
            
            builder.Property(o => o.CustomerId)
                .ValueGeneratedNever()
                .HasColumnType("uuid")
                .HasConversion(
                    id => id.IdValue,
                    value => CustomerId.Create(value));

            builder.Property(o => o.RestaurantId)
                .ValueGeneratedNever()
                .HasColumnType("uuid")
                .HasConversion(
                    id => id.IdValue,
                    value => RestaurantId.Create(value));

            builder.Property(o => o.Status)
                .HasColumnName("Status")
                .HasColumnType("int");

            builder.OwnsOne(o => o.Payment);

            builder.HasOne<Customer>()
                .WithOne()
                .HasForeignKey<Order>("CustomerId")
                .HasPrincipalKey<Customer>(c => c.Id);

         
        }

        private void ConfigureOrderOrderItemsTable(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsMany(order => order.OrderItems, orderitemBuilder =>
            {
                orderitemBuilder.ToTable("OrderOrderItemIds");
                orderitemBuilder
                    .WithOwner()
                    .HasForeignKey("OrderId");

                orderitemBuilder.HasKey("Id");

                orderitemBuilder
                    .Property(item => item.Id)
                    .HasColumnName("OrderItemId")
                    .ValueGeneratedNever()
                    .HasColumnType("uuid")
                    .HasConversion(
                        id => id.IdValue, //To database
                        value => OrderItemId.Create(value));

                //builder.Property(m => m.Id)
                //.ValueGeneratedNever()
                //.HasColumnType("uuid")
                //.HasConversion(
                //    id => id.IdValue, //To database
                //    value => OrderItemId.Create(value) //From Database
                //    );
                //builder.HasKey(m => m.Id);
                orderitemBuilder.Property(m => m.ItemName)
                    .HasColumnName("Name")
                    .HasMaxLength(50);

                orderitemBuilder.Property(m => m.Price)
               .HasColumnType("decimal(5,2)")
               .IsRequired();

                orderitemBuilder.Property(m => m.Quantity)
               .HasColumnType("int")
               .IsRequired();

            }).Navigation(s => s.OrderItems).Metadata
                .SetField("_orderItems");
            builder.Navigation(s => s.OrderItems)
                 .UsePropertyAccessMode(PropertyAccessMode.Field);

        }

        #endregion

        #region Interface Implementations
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            ConfigureOrderTable(builder);
            ConfigureOrderOrderItemsTable(builder);
        }
        #endregion
    }
}
