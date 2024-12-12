using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Customers;
using DineConnect.OrderManagementService.Domain.Customers.Events;
using Infrastructure.IntegrationEvents;
using Infrastructure.IntegrationEvents.Entities.Events;
using MediatR;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class CustomerRepository : Repository<Customer>, IRepository<Customer>
    {
        private readonly ICreateIntegrationEventCommandHandler _commandHandler;
        public CustomerRepository(IMediator mediator,  DineOutOrderDbContext context, ICreateIntegrationEventCommandHandler commandHandler, IAddIntegrationEventCommandHandler integrationEvents)
            : base(context, mediator, integrationEvents) 
        {
            _commandHandler = commandHandler;
        }

        protected override string GetEntityName()
        {
            return "Customer";
        }

        protected override async void PublishEvents(Customer entity, Guid transactionId)
        {
            foreach (CustomerEvent item in entity.DomainEvents)
            {
                var data = new CustomerIntegrationEvent(item.Customer.Id.IdValue, item.Customer.Name, item.Customer.Email, EnumMapper.MapToIntegrationEvent(item.EventCategory));
                await _commandHandler.AddIntegrationEventAsync(data, GetCurrentTransaction());
                await Publish(item);
            }
            entity.ClearDomainEvents();
        }
    }

}
