using DineConnect.OrderManagementService.Application.Features.Customers.Query;
using DineConnect.OrderManagementService.Application.Interfaces.MapperFactories;
using DineConnect.OrderManagementService.Domain.Customers;

namespace DineConnect.OrderManagementService.Contracts.Mapper
{
    /// <summary>
    /// Customer DTO (Model) to Entity and vice versa
    /// </summary>
    public sealed class CustomerModelEntityFactory : IRequestEntityFactory<CustomerCommandModel, Customer>
    {
        public IEnumerable<Customer> CreateEntities(IEnumerable<CustomerCommandModel> requests)
        {
            var result = new List<Customer>();
            foreach (var item in requests)
            {
                result.Add(item.CreateCustomer());
            }
            return result;
        }

        public Customer CreateEntity(CustomerCommandModel request)
        {
            return request.CreateCustomer();
        }
    }

    /// <summary>
    /// Customer Entity to Response (DTO) and vice versa
    /// </summary>
    public sealed class CustomerResponseEntityFactory : IEntityResponseFactory<Customer, CustomerResponse>
    {
        public IEnumerable<CustomerResponse> CreateResponses(IEnumerable<Customer> entities)
        {
            var result = new List<CustomerResponse>();
            foreach (var entity in entities)
            {
                result.Add(entity.CreateResponse());
            }
            return result;
        }

        public CustomerResponse CreateResponse(Customer entity)
        {
            return entity.CreateResponse();
        }
    }

    public static class CustomerMapperExtensions
    {
        public static CustomerResponse CreateResponse(this Customer customer)
        {
            return new CustomerResponse(
                customer.Id.IdValue,
                customer.Name,
                customer.Email,
                new AddressResponse(
                customer.DeliveryAddress.Id.Value,
                customer.DeliveryAddress.Street,
                customer.DeliveryAddress.City,
                customer.DeliveryAddress.PostalCode));
        }

        public static IEnumerable<CustomerResponse> CreateResponses(this IEnumerable<Customer> customers)
        {
            var result = new List<CustomerResponse>();
            foreach (var customer in customers)
            {
                result.Add(customer.CreateResponse());
            }
            return result;
        }

        public static Customer CreateCustomer(this CustomerCommandModel request)
        {
            return Customer.Create(
                request.Name,
                request.email,
                request.Address.street,
                request.Address.City,
                request.Address.PostalCode);
        }

        public static IEnumerable<Customer> CreateCustomers(this IEnumerable<CustomerCommandModel> requests)
        {
            var result = new List<Customer>();
            foreach (var item in requests)
            {
                result.Add(item.CreateCustomer());
            }
            return result;
        }
    }
}
