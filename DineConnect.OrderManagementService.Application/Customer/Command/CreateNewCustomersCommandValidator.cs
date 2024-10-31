using FluentValidation;

namespace DineConnect.OrderManagementService.Application.Customer.Command
{
    public class CreateNewCustomersCommandValidator: AbstractValidator<CreateNewCustomersCommand>
    {
        public CreateNewCustomersCommandValidator()
        {
            RuleFor(x => x.Data)
                .NotEmpty().WithMessage("Data collection cannot be empty.");

            RuleForEach(x => x.Data)
               .SetValidator(new CreateNewCustomerRequestValidator())
               .WithMessage("Invalid customer data.");
        }
    }
}
