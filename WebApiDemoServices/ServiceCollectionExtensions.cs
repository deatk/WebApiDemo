using Microsoft.Extensions.DependencyInjection;
using WebApiDemoServices.Interfaces;
using WebApiDemoServices;

namespace WebApiDemoServices
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPizzaService, PizzaService>();

            return services;
        }
    }
}