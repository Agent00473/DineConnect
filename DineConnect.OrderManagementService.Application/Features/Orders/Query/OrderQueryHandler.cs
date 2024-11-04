using MediatR;
using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Orders;
using DineConnect.OrderManagementService.Application.Interfaces.MapperFactories;

namespace DineConnect.OrderManagementService.Application.Features.Orders.Query
{
    public class OrderQueryHandler: IRequestHandler<OrderQuery, OrderResponseWrapper>
    {
        private readonly IRepository<Order> _repository;
        private readonly IEntityResponseFactory<Order, OrderResponse> _entityResponseFactory;
        public OrderQueryHandler(IRepository<Order> repository,  IEntityResponseFactory<Order, OrderResponse> entityResponseFactory)
        {
            _repository = repository;
            _entityResponseFactory = entityResponseFactory;
        }

        public async Task<OrderResponseWrapper> Handle(OrderQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetPaginatedDataAsync(request.PageNumber, request.PageSize);
            if (data != null)
            {
                var responses = _entityResponseFactory.CreateResponses(data);
                OrderResponseWrapper response = responses.ToList();
                return response;
            }
            OrderResponseWrapper err = OrderErrorDetails.OrderNotFound;
            return err;
        }
    }
}
