using DineConnect.OrderManagementService.Domain.Customers;
using DineConnect.OrderManagementService.Domain.Orders.Entities;
using DineConnect.OrderManagementService.Domain.Orders;
using DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DineConnect.OrderManagementService.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using DineConnect.OrderManagementService.Application.Interfaces;
using DineConnect.OrderManagementService.Infrastructure.ExceptionHandlers;

namespace DineConnect.OrderManagementService.Infrastructure
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddOrderManagementInfrastructureDependencies(this IServiceCollection services,
                                    IConfiguration configration)
        {
            services.AddPersistance(configration)
                    .AddExceptionHandler(configration);
            return services;
        }

        private static IServiceCollection AddExceptionHandler(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExceptionHandler<CreateCustomerExceptionHandler>();
            return services;
        }
        private static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DineOutOrderDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<OrderItem>, MenuItemRepository>();
            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<IRepository<Restaurant>, RestaurantRepository>();
            
            return services;
        }
    }
}
