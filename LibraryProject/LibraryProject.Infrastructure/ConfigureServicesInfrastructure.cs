using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibraryProject.Domain.Interfaces;
using LibraryProject.Domain.Services;
using LibraryProject.Infrastructure.Context;
using LibraryProject.Infrastructure.Finders;
using LibraryProject.Infrastructure.Repositories;

namespace LibraryProject.Infrastructure
{
    public static class ConfigureServicesInfrastructure
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddDbContext<PersistenceContext>(opt =>
            {
                opt.UseInMemoryDatabase("PruebaIngreso");
            });

            services.AddScoped<ILoanFinder, LoanFinder>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<ILoanService, LoanService>();

            return services;
        }
    }
}