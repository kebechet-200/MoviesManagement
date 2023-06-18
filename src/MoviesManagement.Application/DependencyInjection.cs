using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MoviesManagement.Application
{
    public static class DependencyInjection
    {
        private static readonly string _assemblyName = nameof(DependencyInjection);

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(
                x => x.RegisterServicesFromAssembly(Assembly.Load(_assemblyName))
            );

            services.AddValidatorsFromAssembly(Assembly.Load(_assemblyName));

            return services;
        }
    }
}
