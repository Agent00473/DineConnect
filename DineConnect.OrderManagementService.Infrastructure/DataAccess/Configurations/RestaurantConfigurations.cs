using DineConnect.OrderManagementService.Domain.Orders;
using DineConnect.OrderManagementService.Domain.Restaurant.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Configurations
{
    internal class RestaurantConfigurations : IEntityTypeConfiguration<Restaurant>
    {
        private static void ConfigureRestaurantTable(EntityTypeBuilder<Restaurant> builder)
        {
            builder
                .ToTable("Restaurants");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .ValueGeneratedNever()
                .HasColumnType("uuid")
                .HasConversion(
                    id => id.IdValue,
                    value => RestaurantId.Create(value));

            builder.Property(r => r.Name)
                .HasColumnName("Name")
                .HasMaxLength(50);
        }

        #region Interface Implementations
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            ConfigureRestaurantTable(builder);
            ConfigureRestaurantOrderItemTable(builder);
        }

        private void ConfigureRestaurantOrderItemTable(EntityTypeBuilder<Restaurant> builder)
        {
            builder.OwnsMany(r => r.OrderIds, orderBuilder =>
            {
                orderBuilder.ToTable("ResturantOrderIds");

                orderBuilder.WithOwner()
                    .HasForeignKey("ResturantID");

                orderBuilder.HasKey("Id");

                orderBuilder
                 .Property(r => r.IdValue)
                .HasColumnName("OrderItemId")
                    .ValueGeneratedNever();

            });

            builder.Navigation(s => s.OrderIds)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }

        #endregion

    }
}
