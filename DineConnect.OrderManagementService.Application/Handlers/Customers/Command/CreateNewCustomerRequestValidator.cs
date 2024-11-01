using DineConnect.OrderManagementService.Application.Interfaces.Requests;
using DineConnect.OrderManagementService.Domain.Customers;
using FluentValidation;

namespace DineConnect.OrderManagementService.Application.Handlers.Customers.Command
{
    public class CreateNewCustomerRequestValidator : AbstractValidator<INewCustomerRequest>
    {
        public CreateNewCustomerRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{Name} is required.").WithState(NewCustomerRequest => CustomerErrorDetails.InvalidName)
                .NotNull()
                .MaximumLength(50).WithMessage("{Name} must not exceed XXX characters.").WithState(NewCustomerRequest => CustomerErrorDetails.InvalidName);
            RuleFor(x => x.email).EmailAddress().WithMessage("{email} is not valid.").WithState(NewCustomerRequest => CustomerErrorDetails.InvalidEmail);
            RuleFor(x => x.Address).NotEmpty().WithState(NewCustomerRequest => CustomerErrorDetails.InvalidAddress);
            RuleFor(x => x.Address.City).NotEmpty().WithState(NewCustomerRequest => CustomerErrorDetails.InvalidAddress);
            RuleFor(x => x.Address.PostalCode).NotEmpty().WithState(NewCustomerRequest => CustomerErrorDetails.InvalidAddress);
        }
    }
}
