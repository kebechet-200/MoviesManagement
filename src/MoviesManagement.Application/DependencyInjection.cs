using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MoviesManagement.Application
{
    public static class DependencyInjection
    {
        private static readonly Assembly _assembly = typeof(DependencyInjection).Assembly;

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(
                x => x.RegisterServicesFromAssembly(_assembly)
            );

            services.AddValidatorsFromAssembly(_assembly);

            return services;
        }
    }
}
