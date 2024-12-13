using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Orders;
using Infrastructure.IntegrationEvents;
using MediatR;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class RestaurantRepository : Repository<Restaurant>, IRepository<Restaurant>
    {
        public RestaurantRepository(IMediator mediator, DineOutOrderDbContext context, IAddIntegrationEventCommandHandler integrationEvents) : base(context, mediator, integrationEvents) { }
        protected override string GetEntityName()
        {
            return "Restaurant";
        }

        protected async override Task PublishEventsAsync(Restaurant entity, Guid transactionId)
        {
            foreach (var item in entity.DomainEvents)
            {
                await PublishAsync(item);
            }
            entity.ClearDomainEvents();


            //foreach (var item in entity.DomainEvents)
            //{
            //    _mediator.PublishAsync(item);
            //}
            //entity.ClearDomainEvents();
        }
    }

}
