using Microsoft.Extensions.DependencyInjection;
using WebApiDemoRepositories.Interfaces;
using WebApiDemoRepositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WebApiDemoModels;

namespace WebApiDemoRepositories
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPizzaRepository, PizzaRepository>();

            return services;
        }

        public static IServiceCollection AddMongoDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoSettings = configuration.GetSection("MongoDb").Get<MongoDbSettings>()
                              ?? throw new InvalidOperationException("MongoDb settings are not configured properly.");

            services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoSettings.ConnectionString));
            services.AddSingleton(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                return client.GetDatabase(mongoSettings.DatabaseName);
            });

            return services;
        }
    }
}