
namespace DineConnect.OrderManagementService.Domain.Customers.Events
{
    public record CustomerCreatedEvent(Guid EventId, Customer Customer) : CustomerEvent(EventId, Customer)
    {
        public static CustomerCreatedEvent Create(Customer customer)
        {
            return new CustomerCreatedEvent(Guid.NewGuid(), customer);
        }
    }
}
