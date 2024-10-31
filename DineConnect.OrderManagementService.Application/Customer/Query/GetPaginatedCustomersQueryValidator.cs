using DineConnect.OrderManagementService.Application.Restaurant.Query;
using FluentValidation;


namespace DineConnect.OrderManagementService.Application.Customer.Query
{
    public class GetPaginatedCustomersQueryValidator: AbstractValidator<GetPaginatedCustomersQuery>
    {
        public GetPaginatedCustomersQueryValidator()
        {
            RuleFor(x => x.PageNumber).LessThan(0).WithMessage("Page Number {PageNumber} is Invalid");
            RuleFor(x => x.PageSize).LessThan(1).WithMessage("Page Size {PageSize} is Cannot be less than 25");
        }
    }
}
