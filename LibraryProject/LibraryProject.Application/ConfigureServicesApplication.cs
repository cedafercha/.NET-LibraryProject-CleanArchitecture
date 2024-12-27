using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryProject.Application
{
    public static class ConfigureServicesApplication
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ConfigureServicesApplication));
            services.AddMediatR(typeof(ConfigureServicesApplication).Assembly);
            return services;
        }
    }
}