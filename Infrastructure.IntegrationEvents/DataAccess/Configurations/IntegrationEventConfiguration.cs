using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.IntegrationEvents.Events;

namespace Infrastructure.IntegrationEvents.DataAccess.Configurations
{
    internal class IntegrationEventConfiguration : IEntityTypeConfiguration<IntegrationEventDetail>
    {
        #region Interface Implementations
        public void Configure(EntityTypeBuilder<IntegrationEventDetail> builder)
        {
            builder.ToTable("IntegrationEventData").
                 HasKey(c => c.EventId);

            builder.Property(c => c.EventId)
                .ValueGeneratedNever()
                .HasColumnType("uuid");

            builder.Property(e => e.EventTypeName)
                .IsRequired();

            builder.Property(e => e.Content)
                .IsRequired();

            builder.Property(e => e.CreationTime)
                .IsRequired();

            builder.Property(e => e.State)
                .HasConversion<int>(); // Enum to int conversion

            builder.Property(e => e.TimesSent)
                .IsRequired();

            builder.Property(e => e.TransactionId)
                .IsRequired();

            // Ignored/NotMapped properties
            builder.Ignore(e => e.IntegrationEvent);
        }
        #endregion

    }
}
