using DineConnect.OrderManagementService.Application.Features.Orders.Query;
using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Application.Interfaces.MapperFactories;
using DineConnect.OrderManagementService.Domain.Orders;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Features.Orders.Command
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderResponseWrapper>
    {
        private readonly IRepository<Order> _repository;
        private readonly IEntityResponseFactory<Order, OrderResponse> _entityResponseFactory;
        private readonly IRequestEntityFactory<OrderCommandModel, Order> _requestEntityFactory;

        public CreateOrderCommandHandler(IRepository<Order> repository, IRequestEntityFactory<OrderCommandModel, Order> requestEntityFactory, IEntityResponseFactory<Order, OrderResponse> entityResponseFactory)
        {
            _repository = repository;
            _entityResponseFactory = entityResponseFactory;
            _requestEntityFactory = requestEntityFactory;
        }
        public async Task<OrderResponseWrapper> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _requestEntityFactory.CreateEntity(request.data);
            await _repository.AddAsync(order);
            var response = _entityResponseFactory.CreateResponse(order);
            OrderResponseWrapper result = new List<OrderResponse>{ response };
            return result;
        }
    }
}
