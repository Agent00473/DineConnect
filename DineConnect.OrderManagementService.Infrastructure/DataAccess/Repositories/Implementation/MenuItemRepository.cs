using DineConnect.OrderManagementService.Domain.Orders.Entities;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class MenuItemRepository : Repository<OrderItem>
    {
        public MenuItemRepository(DineOutOrderDbContext context) : base(context) { }

    }

}
