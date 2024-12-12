using Infrastructure.IntegrationEvents.Entities.Events;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public static class EnumMapper
    {
        public static Domain.Customers.Events.EventActionCategory MapToDomainEvent(EventActionCategory source)
        {
            return source switch
            {
                EventActionCategory.Created => Domain.Customers.Events.EventActionCategory.Created,
                EventActionCategory.Updated => Domain.Customers.Events.EventActionCategory.Updated,
                EventActionCategory.Deleted => Domain.Customers.Events.EventActionCategory.Deleted,
                _ => throw new ArgumentOutOfRangeException(nameof(source), $"Unhandled value: {source}")
            };
        }

        public static EventActionCategory MapToIntegrationEvent(Domain.Customers.Events.EventActionCategory source)
        {
            return source switch
            {
                Domain.Customers.Events.EventActionCategory.Created => EventActionCategory.Created,
                Domain.Customers.Events.EventActionCategory.Updated => EventActionCategory.Updated,
                Domain.Customers.Events.EventActionCategory.Deleted => EventActionCategory.Deleted,
                _ => throw new ArgumentOutOfRangeException(nameof(source), $"Unhandled value: {source}")
            };
        }
    }

}
