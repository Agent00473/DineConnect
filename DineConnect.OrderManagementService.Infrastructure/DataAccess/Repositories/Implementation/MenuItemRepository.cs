using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Orders.Entities;
using Infrastructure.IntegrationEvents;
using MediatR;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class MenuItemRepository : Repository<OrderItem>, IRepository<OrderItem>
    {
        public MenuItemRepository(IMediator mediator, DineOutOrderDbContext context, IAddIntegrationEventCommandHandler integrationEvents) : base(context, mediator, integrationEvents) { }

        protected override string GetEntityName()
        {
            return "OrderItem";
        }

        protected override void PublishEvents(OrderItem entity, Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }

}
