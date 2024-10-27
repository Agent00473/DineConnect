using DineConnect.OrderManagementService.Application.DataAccess;
using DineConnect.OrderManagementService.Domain.Customer;
using DineConnect.OrderManagementService.Domain.Order.Entities;
using DineConnect.OrderManagementService.Domain.Order;
using DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DineConnect.OrderManagementService.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DineConnect.OrderManagementService.Infrastructure
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                    IConfiguration configration)
        {
            services.AddPersistance(configration);
            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
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
