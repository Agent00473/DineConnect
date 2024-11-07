using DineConnect.OrderManagementService.Domain.Customers;
using DineConnect.OrderManagementService.Domain.Customers.Entities;
using DineConnect.OrderManagementService.Domain.Customers.ValueObjects;
using DineConnect.OrderManagementService.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        #region Private Methods
        private void ConfigureCustomer(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers").
                HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasColumnType("uuid")
                .HasConversion(
                    id => id.IdValue,
                    value => CustomerId.Create(value));
          
            builder.Property(c => c.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(50);
           
            builder.Property(c => c.Email)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(50);


            builder.HasOne(addr => addr.DeliveryAddress)
                .WithOne()
                .HasForeignKey<DeliveryAddress>("DeliveryAddressId")
                .IsRequired();
            
            builder.Navigation(c => c.DeliveryAddress).AutoInclude();
            builder.Metadata
             .FindNavigation(nameof(Customer.DeliveryAddress))!
             .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
        #endregion


        #region Interface Implementations
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            ConfigureCustomer(builder);
        }
        #endregion

    }
}
