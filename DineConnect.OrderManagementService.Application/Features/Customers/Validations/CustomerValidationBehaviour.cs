using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Customers;
using FluentValidation;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Features.Customers.Validations
{
    //TODO: Review Implment one handler for all Request and Responses or Have Specific handler for each request

    /// <summary>
    /// Centralized validation in MediatR pipeline handling the Validation Errors
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="validators"></param>
    public class OrderValidationBehaviour<TRequest>(IEnumerable<IValidator<TRequest>> validators)
                                                             : IPipelineBehavior<TRequest, CustomerResponseWrapper>
                                                             where TRequest : class

    {

        public async Task<CustomerResponseWrapper> Handle(TRequest request, RequestHandlerDelegate<CustomerResponseWrapper> next, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(next);

            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken))).ConfigureAwait(false);

                var failures = validationResults
                    .Where(r => r.Errors.Count > 0)
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (failures.Count > 0)
                {
                    ErrorDetails<CustomerErrorCode> err = (ErrorDetails<CustomerErrorCode>)failures[0].CustomState;
                    CustomerResponseWrapper result = err;
                    return result;
                }
            }
            return await next().ConfigureAwait(false);
        }

    }
}
