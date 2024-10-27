using DineConnect.OrderManagementService.Domain.Customer;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(DineOutOrderDbContext context) : base(context) { }

    }

}
