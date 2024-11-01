using DineConnect.OrderManagementService.Application.Interfaces;
using MediatR;

namespace DineConnect.OrderManagementService.Application.Handlers.Customers.Command
{
    public class CreateNewCustomersHandler : IRequestHandler<CreateNewCustomersCommand, IEnumerable<Domain.Customers.Customer>>
    {
        private readonly IRepository<Domain.Customers.Customer> _repository;

        public CreateNewCustomersHandler(IRepository<Domain.Customers.Customer> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Domain.Customers.Customer>> Handle(CreateNewCustomersCommand request, CancellationToken cancellationToken)
        {
            //Create Cusotmer Fomr Request
            Domain.Customers.Customer entity = null;
            await _repository.AddAsync(entity);
            return Enumerable.Empty<Domain.Customers.Customer>();
        }
    }
}
