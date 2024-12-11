
using DineConnect.OrderManagementService.Domain.Interfaces;

namespace DineConnect.OrderManagementService.Domain.Customers.Events
{
    public enum EventActionCategory
    {
        Created = 0,
        Updated = 1,
        Deleted = 2
    }
    public record CustomerEvent(Guid EventId, EventActionCategory EventCategory, Customer Customer) : IDomainEvent
    {
        public static CustomerEvent Create(Customer customer, EventActionCategory eventCategory)
        {
            return new CustomerEvent(Guid.NewGuid(), eventCategory, customer);
        }
    }
}
