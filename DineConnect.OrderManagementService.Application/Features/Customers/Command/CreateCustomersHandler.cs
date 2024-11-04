using DineConnect.OrderManagementService.Application.Features.Customers.Query;
using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Application.Interfaces.MapperFactories;

using DineConnect.OrderManagementService.Domain.Customers;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Features.Customers.Command
{
    public class CreateCustomersHandler : IRequestHandler<CreateCustomerCommand, CustomerResponseWrapper>
    {
        private readonly IRepository<Customer> _repository;
        private readonly IRequestEntityFactory<CustomerCommandModel, Customer> _requestEntityFactory;
        private readonly IEntityResponseFactory<Customer, CustomerResponse> _entityResponseFactory;

        public CreateCustomersHandler(IRepository<Customer> repository, IRequestEntityFactory<CustomerCommandModel, Customer> requestEntityFactory,
            IEntityResponseFactory<Customer, CustomerResponse> entityResponseFactory)
        {
            _repository = repository;
            _requestEntityFactory = requestEntityFactory;
            _entityResponseFactory = entityResponseFactory;
        }

        async Task<CustomerResponseWrapper> IRequestHandler<CreateCustomerCommand, CustomerResponseWrapper>.Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entities = _requestEntityFactory.CreateEntities(request.Data);
            await _repository.AddAsync(entities);
            var reponses = _entityResponseFactory.CreateResponses(entities);
            CustomerResponseWrapper result = reponses.ToList();
            return result;
        }
    }
}
