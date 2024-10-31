
using DineConnect.OrderManagementService.Application.Customer.Mapper;
using DineConnect.OrderManagementService.Application.DataAccess;
using DineConnect.OrderManagementService.Contracts.Customer;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Customer.Command
{
    public class CreateNewCustomersHandler : IRequestHandler<CreateNewCustomersCommand, IEnumerable<CustomerResponse>>
    {
        private readonly IRepository<Domain.Customers.Customer> _repository;

        public CreateNewCustomersHandler(IRepository<Domain.Customers.Customer> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<CustomerResponse>> Handle(CreateNewCustomersCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Data.CreateCustomers();
            await _repository.AddAsync(entity);
            return entity.CreateCustomerResponses();
        }
    }
}
