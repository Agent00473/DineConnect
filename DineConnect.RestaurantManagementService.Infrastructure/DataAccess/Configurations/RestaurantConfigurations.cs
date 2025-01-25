using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DineConnect.RestaurantManagementService.Domain.Resturants;


namespace DineConnect.RestaurantManagementService.Infrastructure.DataAccess.Configurations
{
    internal class RestaurantConfigurations : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("MasterRestaurants");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .ValueGeneratedNever()
                .HasColumnType("uuid")
                .HasConversion(
                    id => id.IdValue,
                    value => RestaurantId.Create(value));

            builder.Property(r => r.Name)
                 .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Cuisine)
                 .HasColumnName("Cuisine")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Rating).
                HasColumnName("Rating")
                .IsRequired();

            builder.Property(r => r.Description)
                .HasColumnName("Description")
                .HasMaxLength(500);

            builder.OwnsOne(r => r.Location, locationBuilder =>
            {
                locationBuilder.Property(l => l.Street)
                    .HasColumnName("Street")
                    .IsRequired()
                    .HasMaxLength(100);

                locationBuilder.Property(l => l.City)
                    .HasColumnName("City")
                    .IsRequired()
                    .HasMaxLength(50);

                locationBuilder.Property(l => l.State)
                    .HasColumnName("State")
                    .IsRequired()
                    .HasMaxLength(50);

                locationBuilder.Property(l => l.Country)
                    .HasColumnName("Country")
                    .IsRequired()
                    .HasMaxLength(50);

                locationBuilder.Property(l => l.Pin).
                    HasColumnName("ZipCode")
                    .IsRequired()
                    .HasMaxLength(10);
            });
        }
        

    }
}
