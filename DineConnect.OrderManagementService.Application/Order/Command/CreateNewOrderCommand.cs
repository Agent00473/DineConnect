using DineConnect.OrderManagementService.Contracts.Order;
using MediatR;


namespace DineConnect.OrderManagementService.Application.Customer.Command
{
    public record CreateNewOrderCommand(NewOrderRequest data): IRequest<OrderResponse>
    {
    }
}
