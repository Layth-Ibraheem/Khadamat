using Khadamat_FileService.Application.Common.FileContext;
using Khadamat_FileService.Application.Common.Interfaces;
using Khadamat_FileService.Domain.Common.Interfaces;
using Khadamat_FileService.Infrastructure.Common.EntityTag;
using Khadamat_FileService.Infrastructure.Common.Persistence;
using Khadamat_FileService.Infrastructure.FilesContextMetadata.Persistence;
using Khadamat_FileService.Infrastructure.KhadamatFiles;
using Khadamat_FileService.Infrastructure.KhadamatFiles.Persistence;
using Khadamat_SellerPortal.Infrastructure.IntegrationEvents.BackgroundServices;
using Khadamat_SellerPortal.Infrastructure.IntegrationEvents.IntegrationEventsPublisher;
using Khadamat_SellerPortal.Infrastructure.IntegrationEvents.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Khadamat_FileService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddDbContext<Khadamat_FileServiceDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IFilesManagerService, FilesManagerService>();
            services.AddScoped<IEntityTagGenerator, EntityTagGenerator>();
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<Khadamat_FileServiceDbContext>());
            services.AddScoped<IFilesContextMetadataRepo, FilesContextMetadataRepo>();
            services.AddHostedServices();
            services.AddMediator();

            var rabbitMQSettings = new RabbitMQSettings();

            configuration.Bind(RabbitMQSettings.Section, rabbitMQSettings);

            services.AddSingleton(Options.Create(rabbitMQSettings));
            return services;
        }
        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

            return services;
        }
        private static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddSingleton<IIntegrationEventsPublisher, IntegrationEventsPublisher>();
            services.AddHostedService<PublishIntegrationEventsBackgroundService>();
            services.AddHostedService<ConsumeIntegrationEventsBackgroundService>();
            return services;
        }

    }
}
