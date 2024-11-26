using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Orders;
using MediatR;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation
{
    public class RestaurantRepository : Repository<Restaurant>, IRepository<Restaurant>
    {
        public RestaurantRepository(IMediator mediator, DineOutOrderDbContext context) : base(context, mediator) { }

        protected override string GetEntityName()
        {
            return "Restaurant";
        }

        protected async override void PublishEvents(Restaurant entity)
        {
            foreach (var item in entity.DomainEvents)
            {
                await Publish(item);
            }
            entity.ClearDomainEvents();


            //foreach (var item in entity.DomainEvents)
            //{
            //    _mediator.Publish(item);
            //}
            //entity.ClearDomainEvents();
        }
    }

}
