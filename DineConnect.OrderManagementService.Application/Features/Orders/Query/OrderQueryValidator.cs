using FluentValidation;

namespace DineConnect.OrderManagementService.Application.Features.Orders.Query
{
    public class OrderQueryValidator : AbstractValidator<OrderQuery>
    {
        public OrderQueryValidator()
        {
            RuleFor(x => x.PageNumber).LessThan(0).WithMessage("Page Number {PageNumber} is Invalid");
            RuleFor(x => x.PageSize).LessThan(1).WithMessage("Page Size {PageSize} is Cannot be less than 25");
        }
    }
}
