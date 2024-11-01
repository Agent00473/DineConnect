using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Domain.Orders;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Handlers.Orders.Command
{
    public class CreateNewOrderCommandHandler : IRequestHandler<CreateNewOrderCommand, Order>
    {
        private readonly IRepository<Order> _repository;

        public CreateNewOrderCommandHandler(IRepository<Order> repository)
        {
            _repository = repository;
        }
        public async Task<Order> Handle(CreateNewOrderCommand request, CancellationToken cancellationToken)
        {
            var order = Order.Create(request.data.CustomerId, request.data.RestaurentId);// request.data.CreateOrder();
            await _repository.AddAsync(order);
            return order;
        }
    }
}
