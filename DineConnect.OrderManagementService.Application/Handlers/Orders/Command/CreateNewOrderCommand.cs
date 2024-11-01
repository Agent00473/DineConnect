using DineConnect.OrderManagementService.Application.Interfaces.Requests;
using DineConnect.OrderManagementService.Domain.Orders;
using MediatR;


namespace DineConnect.OrderManagementService.Application.Handlers.Orders.Command
{
    public record CreateNewOrderCommand(INewOrderRequest data) : IRequest<Order>
    {
    }
}
