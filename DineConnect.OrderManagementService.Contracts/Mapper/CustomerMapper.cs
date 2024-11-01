using DineConnect.OrderManagementService.Application.Interfaces.Requests;
using DineConnect.OrderManagementService.Application.Interfaces.Responses;
using DineConnect.OrderManagementService.Contracts.Responses;

namespace DineConnect.OrderManagementService.Contracts.Mapper

{
    public static class CustomerMapper
    {
        public static ICustomerResponse CreateCustomerResponse(this Domain.Customers.Customer customer)
        {
            return new CustomerResponse(
                customer.Id.IdValue,
                customer.Name,
                customer.Email,
                new DeliveryAddressResponse(
                customer.DeliveryAddress.Id.Value,
                customer.DeliveryAddress.Street,
                customer.DeliveryAddress.City,
                customer.DeliveryAddress.PostalCode));
        }

        public static IEnumerable<ICustomerResponse> CreateCustomerResponses(this IEnumerable<Domain.Customers.Customer> customers)
        {
            var result = new List<ICustomerResponse>();
            foreach (var customer in customers)
            {
                result.Add(new CustomerResponse(
                    customer.Id.IdValue,
                    customer.Name,
                    customer.Email,
                new DeliveryAddressResponse(
                    customer.DeliveryAddress.Id.Value,
                    customer.DeliveryAddress.Street,
                customer.DeliveryAddress.City,
                    customer.DeliveryAddress.PostalCode)));
            }
            return result;
        }

        public static Domain.Customers.Customer CreateCustomer(this INewCustomerRequest request)
        {
            return Domain.Customers.Customer.Create(
                request.Name,
                request.email,
                request.Address.street,
                request.Address.City,
                request.Address.PostalCode);
        }

        public static IEnumerable<Domain.Customers.Customer> CreateCustomers(this IEnumerable<INewCustomerRequest> requests)
        {
            var result = new List<Domain.Customers.Customer>();
            foreach (var item in requests)
            {
                result.Add(item.CreateCustomer());
            }
            return result;
        }
    }
}
