using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Application.Interfaces.MapperFactories;
using DineConnect.OrderManagementService.Domain.Customers;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Features.Customers.Query
{
    public class CustomerQueryHandler : IRequestHandler<CustomerQuery, CustomerResponseWrapper>
    {
        private readonly IRepository<Customer> _repository;
        private readonly IEntityResponseFactory<Customer, CustomerResponse> _entityResponseFactory;

        public CustomerQueryHandler(IRepository<Customer> repository, IEntityResponseFactory<Customer, CustomerResponse> entityResponseFactory)
        {
            _repository = repository;
            _entityResponseFactory = entityResponseFactory;
        }

        public async Task<CustomerResponseWrapper> Handle(CustomerQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetPaginatedDataAsync(request.PageNumber, request.PageSize);
            if (entities != null)
            {
                var responses = _entityResponseFactory.CreateResponses(entities);
                CustomerResponseWrapper response = responses.ToList();
                return response;
            }
            CustomerResponseWrapper err = CustomerErrorDetails.CustomerNotFound;
            return err;
        }
    }
}
