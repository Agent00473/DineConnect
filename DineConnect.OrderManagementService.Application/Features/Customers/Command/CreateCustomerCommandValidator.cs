using DineConnect.OrderManagementService.Domain.Customers;
using FluentValidation;

namespace DineConnect.OrderManagementService.Application.Features.Customers.Command
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Data)
                .NotEmpty().NotNull().WithMessage("Data collection cannot be empty.").WithState(state => CustomerErrorDetails.NullData);

            RuleForEach(x => x.Data)
               .SetValidator(new CustomerCommandModelValidator())
               .WithMessage("Invalid customer data.");
        }
    }

    public class CustomerCommandModelValidator : AbstractValidator<CustomerCommandModel>
    {
        public CustomerCommandModelValidator()
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
