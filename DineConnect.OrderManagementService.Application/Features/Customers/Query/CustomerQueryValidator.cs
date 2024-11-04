using FluentValidation;


namespace DineConnect.OrderManagementService.Application.Features.Customers.Query
{
    public class CustomerQueryValidator : AbstractValidator<CustomerQuery>
    {
        public CustomerQueryValidator()
        {
            RuleFor(x => x.PageNumber).LessThan(0).WithMessage("Page Number {PageNumber} is Invalid");
            RuleFor(x => x.PageSize).LessThan(1).WithMessage("Page Size {PageSize} is Cannot be less than 25");
        }
    }
}
