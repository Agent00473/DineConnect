using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Orders;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class OrderRepository : Repository<Order>, IRepository<Order>
    {
        public OrderRepository(DineOutOrderDbContext context) : base(context) { }

    }

}
