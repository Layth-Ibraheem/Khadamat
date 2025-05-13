using Khadamat_UserManagement.API.CurrentUserProviderInterface;
using Khadamat_UserManagement.Application.Common.Interfaces;

namespace Khadamat_UserManagement.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresintationDependencies(this IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
            services.AddProblemDetails();
            //services.AddMappings(); // uncomment when adding mappings
            return services;
        }
    }
}
