using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Application.Interfaces.Responses;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Handlers.Customers.Query
{
    public class GetPaginatedCustomersQueryHandler : IRequestHandler<GetPaginatedCustomersQuery, IEnumerable<Domain.Customers.Customer>>
    {
        private readonly IRepository<Domain.Customers.Customer> _repository;

        public GetPaginatedCustomersQueryHandler(IRepository<Domain.Customers.Customer> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Domain.Customers.Customer>> Handle(GetPaginatedCustomersQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetPaginatedDataAsync(request.PageNumber, request.PageSize);
            //if (result!=null)
            //    return result.CreateCustomerResponses();
            //return new List<ICustomerResponse>();
            return Enumerable.Empty<Domain.Customers.Customer>();
        }
    }
}
