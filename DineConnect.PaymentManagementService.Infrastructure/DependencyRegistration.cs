using DineConnect.PaymentManagementService.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DineConnect.PaymentManagementService.Infrastructure
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
            services.AddDbContext<DineOutPaymentDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            //services.AddScoped<IRepository<Order>, OrderRepository>();


            return services;
        }
    }
}
