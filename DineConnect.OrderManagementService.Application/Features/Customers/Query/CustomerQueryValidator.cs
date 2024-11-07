using FluentValidation;


namespace DineConnect.OrderManagementService.Application.Features.Customers.Query
{
    public class CustomerQueryValidator : AbstractValidator<CustomerQuery>
    {
        public CustomerQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Page Number {PageNumber} is Invalid");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page Size {PageSize} cannot be less than 1");
        }
    }
}
