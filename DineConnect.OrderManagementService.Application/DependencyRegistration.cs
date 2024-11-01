using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using System.Reflection;
using DineConnect.OrderManagementService.Application.Common;

namespace DineConnect.OrderManagementService.Application
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddOrderManagementApplicationDependencies(this IServiceCollection services,
                                    IConfiguration configration)
        {
            services.RegisterMediatR(configration);
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }

        private static IServiceCollection RegisterMediatR(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
