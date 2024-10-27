using DineConnect.OrderManagementService.Domain.Customer;
using DineConnect.OrderManagementService.Domain.Customer.Entities;
using DineConnect.OrderManagementService.Domain.Customer.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Configurations
{
    internal class DeliveryAddressConfigurator : IEntityTypeConfiguration<DeliveryAddress>
    {
        #region Private Methods
        private void ConfigureDeliveryAddress(EntityTypeBuilder<DeliveryAddress> builder)
        {
            builder.ToTable("DeliveryAddress").
                HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasColumnType("uuid")
                .HasConversion(
                    id => id.Value,
                    value => DeliveryAddressId.Create(value));

            builder.Property(c => c.Street)
                .HasColumnName("Street")
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(c => c.PostalCode)
                .HasColumnName("PostalCode")
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(c => c.City)
                 .HasColumnName("City")
                 .IsRequired()
                 .HasMaxLength(20);
            
        }
        #endregion

        #region Interface Implementations
        public void Configure(EntityTypeBuilder<DeliveryAddress> builder)
        {
            ConfigureDeliveryAddress(builder);
        }
        #endregion

    }
}
