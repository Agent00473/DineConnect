using DineConnect.OrderManagementService.Application.DataAccess;
using DineConnect.OrderManagementService.Domain.Customers;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class CustomerRepository : Repository<Customer>, IRepository<Customer>
    {
        public CustomerRepository(DineOutOrderDbContext context) : base(context) { }

    }

}
