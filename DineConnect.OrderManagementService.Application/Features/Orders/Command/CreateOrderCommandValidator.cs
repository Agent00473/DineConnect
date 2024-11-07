using FluentValidation;

namespace DineConnect.OrderManagementService.Application.Features.Orders.Command
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.data)
               .SetValidator(new OrderCommandModelValidator())
               .WithMessage("Invalid Order Request.");
        }
    }

    public class OrderCommandModelValidator : AbstractValidator<OrderCommandModel>
    {
        public OrderCommandModelValidator()
        {
            RuleFor(x => x.RestaurantId).NotEmpty().WithMessage(x=>$"Restaurant Id {x.RestaurantId} cannot be empty");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage(x => $"Customer Id {x.CustomerId} cannot be empty");
            RuleFor(x => x.OrderStatus).GreaterThan(0).WithMessage(x => $"Invalid Order status {x.OrderStatus}.");
        }
    }

    
    //TODO: Add Model Validations
    public class PaymentCommandModelValidator : AbstractValidator<PaymentCommandModel>
    {
        public PaymentCommandModelValidator()
        {
            
        }
    }

    public class OrderItemCommandModelValidator : AbstractValidator<OrderItemCommandModel>
    {
        public OrderItemCommandModelValidator()
        {

        }
    }
}
