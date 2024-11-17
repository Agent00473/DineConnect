using FluentValidation;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Common
{
    ///<summary>
    ///Handles the fluent validation errors from MediatR Commands and Query. 
    ///Exception thrown is handled by GlobalExceptionHandler
    ///</summary>
    ///<typeparam name="TRequest"></typeparam>
    ///<typeparam name="TResponse"></typeparam>
    ///<param name="validators"></param>
    public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest> validator)
                                                           : IPipelineBehavior<TRequest, TResponse>
                                                            where TRequest : class
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(next);
            if (validator is null)
            {
                return await next().ConfigureAwait(false);
            }
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            return await next().ConfigureAwait(false);
        }
    }

    ///Currenlty the Validators are 1-1 each Commands or Query has one validator, if More number to be attached switch to this 
    //public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    //                                                        : IPipelineBehavior<TRequest, TResponse>
    //                                                        where TRequest : class
    //{
    //    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    //    {
    //        ArgumentNullException.ThrowIfNull(next);

    //        if (validators.Any())
    //        {
    //            var context = new ValidationContext<TRequest>(request);

    //            var validationResults = await Task.WhenAll(
    //                validators.Select(v =>
    //                    v.ValidateAsync(context, cancellationToken))).ConfigureAwait(false);

    //            var failures = validationResults
    //                .Where(r => r.Errors.Count > 0)
    //                .SelectMany(r => r.Errors)
    //                .ToList();

    //            if (failures.Count > 0)
    //                throw new ValidationException(failures);
    //        }
    //        return await next().ConfigureAwait(false);
    //    }

    //}


}
