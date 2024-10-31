using DineConnect.OrderManagementService.Contracts.Customer;
using FluentValidation;

namespace DineConnect.OrderManagementService.Application.Customer.Command
{
    public class CreateNewCustomerRequestValidator : AbstractValidator<NewCustomerRequest>
    {
        public CreateNewCustomerRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{Name} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{Name} must not exceed XXX characters.");
            RuleFor(x => x.email).EmailAddress().WithMessage("{email} is not valid.");
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Address.City).NotEmpty();
            RuleFor(x => x.Address.PostalCode).NotEmpty();
        }
    }
}
