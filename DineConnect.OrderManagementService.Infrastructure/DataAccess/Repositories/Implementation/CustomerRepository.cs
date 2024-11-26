using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Customers;
using DineConnect.OrderManagementService.Domain.Customers.Events;
using MediatR;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class CustomerRepository : Repository<Customer>, IRepository<Customer>
    {
        public CustomerRepository(IMediator mediator,  DineOutOrderDbContext context) : base(context, mediator) { }

        protected override string GetEntityName()
        {
            return "Customer";
        }

        protected override async void PublishEvents(Customer entity)
        {
            foreach (CustomerEvent item in entity.DomainEvents)
            {
                await Publish(item);
            }
            entity.ClearDomainEvents();
        }
    }

}
