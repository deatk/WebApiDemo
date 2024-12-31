using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace WebApiDemoRepositories
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbRepository(this IServiceCollection services, IConfiguration configuration)
        {
            // Recupera le impostazioni dal file di configurazione
            var mongoSettings = configuration.GetSection("MongoDb").Get<MongoDbSettings>()
                              ?? throw new InvalidOperationException("MongoDb settings are not configured properly.");

            // Registra il client MongoDB come singleton
            services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoSettings.ConnectionString));

            // Registra il database MongoDB come singleton
            services.AddSingleton(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                return client.GetDatabase(mongoSettings.DatabaseName);
            });

            return services;
        }
    }
}
