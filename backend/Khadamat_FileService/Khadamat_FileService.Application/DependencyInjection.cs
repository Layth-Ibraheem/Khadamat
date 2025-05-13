using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Khadamat_FileService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));

            });
            return services;
        }
    }
}
