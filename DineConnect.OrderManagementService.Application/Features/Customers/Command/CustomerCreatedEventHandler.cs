using DineConnect.OrderManagementService.Application.Common;
using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Customers.Events;
using MediatR;
using DineConnect.OrderManagementService.Domain.Customers;

namespace DineConnect.OrderManagementService.Application.Features.Customers.Command
{
    public class CustomerCreatedEventHandler : INotificationHandler<NotificationModel<CustomerEvent>>
    {
        private readonly IRepository<Customer> _repository;

        public CustomerCreatedEventHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }
        public Task Handle(NotificationModel<CustomerEvent> notification, CancellationToken cancellationToken)
        {

            Console.WriteLine(notification.DomainEvent.Customer.Name);
            return Task.CompletedTask;
        }
    }
}
