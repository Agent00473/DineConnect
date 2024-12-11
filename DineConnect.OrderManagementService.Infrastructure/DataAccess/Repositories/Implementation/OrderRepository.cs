using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Orders;
using Infrastructure.IntegrationEvents.Database.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class OrderRepository : Repository<Order>, IRepository<Order>
    {
    public OrderRepository(IMediator mediator, DineOutOrderDbContext context, IAddIntegrationEventCommandHandler integrationEvents) : base(context, mediator, integrationEvents) { }

        protected override string GetEntityName()
        {
            return "Order";
        }

        protected override void PublishEvents(Order entity, Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }

}
