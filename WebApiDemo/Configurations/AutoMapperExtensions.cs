using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace WebApiDemo.Configurations
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program).Assembly); // Ensure the correct assembly is used
            return services;
        }
    }
}