using DineConnect.OrderManagementService.Domain.Order.Entities;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class MenuItemRepository : Repository<OrderItem>
    {
        public MenuItemRepository(DineOutOrderDbContext context) : base(context) { }

    }

}
