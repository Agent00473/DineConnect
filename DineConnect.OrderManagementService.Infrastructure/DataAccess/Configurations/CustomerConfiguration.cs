using DineConnect.OrderManagementService.Domain.Customer;
using DineConnect.OrderManagementService.Domain.Customer.Entities;
using DineConnect.OrderManagementService.Domain.Customer.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
