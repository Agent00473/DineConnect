using DineConnect.OrderManagementService.Application.Customer.Mapper;
using DineConnect.OrderManagementService.Application.DataAccess;
using DineConnect.OrderManagementService.Contracts.Customer;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Restaurant.Query
{
    public class GetPaginatedCustomersQueryHandler : IRequestHandler<GetPaginatedCustomersQuery, IEnumerable<CustomerResponse>>
    {
        private readonly IRepository<Domain.Customers.Customer> _repository;

        public GetPaginatedCustomersQueryHandler(IRepository<Domain.Customers.Customer> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerResponse>> Handle(GetPaginatedCustomersQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetPaginatedDataAsync(request.PageNumber, request.PageSize);
            if (result!=null)
                return result.CreateCustomerResponses();
            return new List<CustomerResponse>();
        }
    }
}
