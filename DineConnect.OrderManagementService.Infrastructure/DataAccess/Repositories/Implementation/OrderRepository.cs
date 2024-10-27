using DineConnect.OrderManagementService.Domain.Order;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(DineOutOrderDbContext context) : base(context) { }

    }

}
