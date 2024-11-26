using DineConnect.OrderManagementService.Application.Common;
using DineConnect.OrderManagementService.Domain.Customers.Events;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Features.Customers.Command
{
    public class CustomerCreatedEventHandler : INotificationHandler<NotificationModel<CustomerEvent>>
    {

        public Task Handle(NotificationModel<CustomerEvent> notification, CancellationToken cancellationToken)
        {
            Console.WriteLine(notification.DomainEvent.Customer.Name);
            return Task.CompletedTask;
        }
    }
}
