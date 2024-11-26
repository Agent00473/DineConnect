using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Orders.Entities;
using MediatR;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class MenuItemRepository : Repository<OrderItem>, IRepository<OrderItem>
    {
        public MenuItemRepository(IMediator mediator, DineOutOrderDbContext context) : base(context, mediator) { }

        protected override string GetEntityName()
        {
            return "OrderItem";
        }

        protected override void PublishEvents(OrderItem entity)
        {
            throw new NotImplementedException();
        }
    }

}
