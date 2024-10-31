using DineConnect.OrderManagementService.Application.Customer.Mapper;
using DineConnect.OrderManagementService.Application.DataAccess;
using DineConnect.OrderManagementService.Contracts.Order;
using DineConnect.OrderManagementService.Domain.Orders;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Customer.Command
{
    public class CreateNewOrderCommandHandler : IRequestHandler<CreateNewOrderCommand, OrderResponse>
    {
        private readonly IRepository<Order> _repository;

        public CreateNewOrderCommandHandler(IRepository<Order> repository)
        {
            _repository = repository;
        }
        public async Task<OrderResponse> Handle(CreateNewOrderCommand request, CancellationToken cancellationToken)
        {
            var order = request.data.CreateOrder();
            await _repository.AddAsync(order);
            return order.CreateNewOrderResponse();
        }
    }
}
