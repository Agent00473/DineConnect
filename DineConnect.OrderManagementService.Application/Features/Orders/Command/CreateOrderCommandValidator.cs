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
            RuleFor(x => x.RestaurentId).NotEmpty().WithMessage("Restaurant Id {RestaurentId} cannot be empty");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer Id {CustomerId} cannot be empty");
            RuleFor(x => x.OrderStatus).LessThan(1).WithMessage("Invalid Order status {OrderStatus}.");
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
