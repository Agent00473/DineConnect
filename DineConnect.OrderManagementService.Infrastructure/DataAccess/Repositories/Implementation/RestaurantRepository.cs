using DineConnect.OrderManagementService.Domain.Orders;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class RestaurantRepository : Repository<Restaurant>
    {
        public RestaurantRepository(DineOutOrderDbContext context) : base(context) { }

    }

}
