using DineConnect.OrderManagementService.Domain.Interfaces;

namespace DineConnect.OrderManagementService.Domain.Customers.Events
{
    public record CustomerCreated(Guid EventId, Customer customer) : IDomainEvent
    {
        public static CustomerCreated Create(Customer customer)
        {
            return new CustomerCreated(Guid.NewGuid(), customer);
        }
    }
}
