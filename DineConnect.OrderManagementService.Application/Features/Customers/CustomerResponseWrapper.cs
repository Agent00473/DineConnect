using DineConnect.OrderManagementService.Application.Common;
using DineConnect.OrderManagementService.Application.Features.Customers.Query;
using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Customers;

namespace DineConnect.OrderManagementService.Application.Features.Customers
{

    public sealed class CustomerResponseWrapper : ResponseWrapper<IEnumerable<CustomerResponse>, CustomerErrorCode>
    {
        private CustomerResponseWrapper(IEnumerable<CustomerResponse> value) : base(value)
        {
        }

        private CustomerResponseWrapper(ErrorDetails<CustomerErrorCode> error) : base(error)
        {
        }
        public static implicit operator CustomerResponseWrapper(List<CustomerResponse> value) => new CustomerResponseWrapper(value);

        public static implicit operator CustomerResponseWrapper(ErrorDetails<CustomerErrorCode> error) => new CustomerResponseWrapper(error);

    }
}
