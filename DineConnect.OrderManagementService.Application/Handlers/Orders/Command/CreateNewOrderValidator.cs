using DineConnect.OrderManagementService.Application.Interfaces.Requests;
using FluentValidation;

namespace DineConnect.OrderManagementService.Application.Handlers.Orders.Command
{
    public class CreateNewOrderCommandValidator : AbstractValidator<CreateNewOrderCommand>
    {
        public CreateNewOrderCommandValidator()
        {
            RuleFor(x => x.data)
               .SetValidator(new NewOrderRequestValidator())
               .WithMessage("Invalid Order Request.");
        }
    }

    public class NewOrderRequestValidator : AbstractValidator<INewOrderRequest>
    {
        public NewOrderRequestValidator()
        {
            RuleFor(x => x.RestaurentId).NotEmpty().WithMessage("Restaurant Id {RestaurentId} cannot be empty");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer Id {CustomerId} cannot be empty");
            RuleFor(x => x.OrderStatus).LessThan(1).WithMessage("Invalid Order status {OrderStatus}.");
        }
    }
}
