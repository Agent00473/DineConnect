
using DineConnect.OrderManagementService.Domain.Interfaces;

namespace DineConnect.OrderManagementService.Domain.Customers.Events
{
    public record CustomerEvent(Guid EventId, Customer Customer) : IDomainEvent
    {
    }
}
