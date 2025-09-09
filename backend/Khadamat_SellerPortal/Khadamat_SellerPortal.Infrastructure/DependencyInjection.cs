using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Domain.Common.Interfaces;
using Khadamat_SellerPortal.Infrastructure.Common;
using Khadamat_SellerPortal.Infrastructure.Common.Files;
using Khadamat_SellerPortal.Infrastructure.Common.Persistence;
using Khadamat_SellerPortal.Infrastructure.IntegrationEvents.BackgroundServices;
using Khadamat_SellerPortal.Infrastructure.IntegrationEvents.IntegrationEventsPublisher;
using Khadamat_SellerPortal.Infrastructure.IntegrationEvents.RabbitMQ;
using Khadamat_SellerPortal.Infrastructure.OnlineSellers.Persistence;
using Khadamat_SellerPortal.Infrastructure.Sellers.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Khadamat_SellerPortal.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Khadamat_SellerPortalDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<IOnlineSellerRepository, OnlineSellerRepository>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<Khadamat_SellerPortalDbContext>());
            services.AddScoped<IDomainFilesService, DomainFileService>();

            services.AddHostedServices();

            services.AddMediator();
            

            var rabbitMqSettings = new RabbitMQSettings();

            configuration.Bind(RabbitMQSettings.Section, rabbitMqSettings);
            services.AddSingleton(Options.Create(rabbitMqSettings));
            return services;
        }
        private static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddSingleton<IIntegrationEventsPublisher, IntegrationEventsPublisher>();
            services.AddHostedService<PublishIntegrationEventsBackgroundService>();
            services.AddHostedService<ConsumeIntegrationEventsBackgroundService>();
            return services;
        }

        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

            return services;
        }
    }
}
