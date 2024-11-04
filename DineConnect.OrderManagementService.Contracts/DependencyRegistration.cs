using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DineConnect.OrderManagementService.Application.Interfaces.MapperFactories;
using DineConnect.OrderManagementService.Domain.Customers;
using DineConnect.OrderManagementService.Contracts.Mapper;
using DineConnect.OrderManagementService.Application.Features.Customers.Query;
using DineConnect.OrderManagementService.Application.Features.Orders.Command;
using DineConnect.OrderManagementService.Domain.Orders;
using DineConnect.OrderManagementService.Application.Features.Orders.Query;

namespace DineConnect.OrderManagementService.Application
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddOrderManagementContractsDependencies(this IServiceCollection services, IConfiguration configration)
        {
            //Customer Converters Entity to DTO & Viceversa
            services.AddTransient<IRequestEntityFactory<CustomerCommandModel, Customer>, CustomerModelEntityFactory>();
            services.AddTransient<IEntityResponseFactory<Customer, CustomerResponse>, CustomerResponseEntityFactory>();
            
            //Order Converters Entity to DTO & Viceversa
            services.AddTransient<IRequestEntityFactory<OrderCommandModel, Order>, OrderModelEntityFactory>();
            services.AddTransient<IEntityResponseFactory<Order, OrderResponse>, OrderResponseEntityFactory>();

            return services;
        }

    }
}
