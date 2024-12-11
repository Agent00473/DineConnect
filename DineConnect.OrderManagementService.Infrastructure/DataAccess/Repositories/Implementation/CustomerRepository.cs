using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Customers;
using DineConnect.OrderManagementService.Domain.Customers.Events;
using Infrastructure.IntegrationEvents.Database.Commands;
using Infrastructure.IntegrationEvents.Events;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class CustomerRepository : Repository<Customer>, IRepository<Customer>
    {
        public CustomerRepository(IMediator mediator,  DineOutOrderDbContext context, IAddIntegrationEventCommandHandler integrationEvents) : base(context, mediator, integrationEvents) { }

        protected override string GetEntityName()
        {
            return "Customer";
        }

        protected override async void PublishEvents(Customer entity, Guid transactionId)
        {
            foreach (CustomerEvent item in entity.DomainEvents)
            {
                var data = new CustomerIntegrationEvent(item.Customer.Id.IdValue, item.Customer.Name, item.Customer.Email, EnumMapper.MapToIntegrationEvent(item.EventCategory));
                await _integrationEvents.AddIntegrationEventAsync(data, transactionId);
                await Publish(item);
            }
            entity.ClearDomainEvents();
        }
    }

}
