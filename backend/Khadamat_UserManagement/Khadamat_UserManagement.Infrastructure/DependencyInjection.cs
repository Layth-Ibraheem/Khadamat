using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Application.Common.Logging;
using Khadamat_UserManagement.Domain.Common.Interfaces;
using Khadamat_UserManagement.Infrastructure.Authentication.ResetPasswordCodeProvider;
using Khadamat_UserManagement.Infrastructure.Authentication.TokenGenerator;
using Khadamat_UserManagement.Infrastructure.Common.Email;
using Khadamat_UserManagement.Infrastructure.Common.Logging;
using Khadamat_UserManagement.Infrastructure.Common.PasswordHash;
using Khadamat_UserManagement.Infrastructure.Common.Persistence;
using Khadamat_UserManagement.Infrastructure.RegisterApplications.Persistence;
using Khadamat_UserManagement.Infrastructure.Users.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Khadamat_UserManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration)
                .AddAuthentication(configuration)
                .AddEmailSettings(configuration);

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Khadamat_UserDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<Khadamat_UserDbContext>());
            services.AddMemoryCache();
            services.AddScoped<IPasswordResetCodeProvider, PasswordResetCodeProvider>();
            services.AddScoped<ILogsCollector, LogsCollector>();
            services.AddScoped<IRegisterApplicationRepository, RegisterApplicationRepository>();
            return services;
        }
        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.Section, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                });


            return services;
        }

        private static IServiceCollection AddEmailSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = new EmailSettings();
            configuration.Bind(EmailSettings.Section, emailSettings);
            services.AddSingleton(Options.Create(emailSettings));
            services.AddSingleton<IEmailSender, EmailSender>();

            return services;
        }
    }
}
