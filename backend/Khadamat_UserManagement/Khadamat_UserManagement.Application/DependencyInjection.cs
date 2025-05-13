using Khadamat_UserManagement.Application.Common.Behaviors;
using Khadamat_UserManagement.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace Khadamat_UserManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));

                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
                options.AddOpenBehavior(typeof(RolesCheckBehavior<,>));

            });
            var sendingEmailSettings = new SendingEmailSettings();
            configuration.Bind(SendingEmailSettings.Section, sendingEmailSettings);
            services.AddSingleton(Options.Create(sendingEmailSettings));
            return services;
        }
    }
}
