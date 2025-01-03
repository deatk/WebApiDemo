using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.IO;

namespace WebApiDemo.Configurations
{
    public static class SerilogExtensions
    {
        public static IServiceCollection AddSerilog(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "logs", "log.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

            return services;
        }
    }
}