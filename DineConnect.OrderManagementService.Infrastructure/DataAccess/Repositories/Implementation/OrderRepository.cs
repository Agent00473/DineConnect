using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Orders;
using MediatR;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class OrderRepository : Repository<Order>, IRepository<Order>
    {
        public OrderRepository(IMediator mediator, DineOutOrderDbContext context) : base(context, mediator) { }

        protected override string GetEntityName()
        {
            return "Order";
        }

        protected override void PublishEvents(Order entity)
        {
            throw new NotImplementedException();
        }
    }

}
