using Khadamat_FileService.Application.Common.Interfaces;
using Khadamat_FileService.Domain.Common.Interfaces;
using Khadamat_FileService.Infrastructure.Common.EntityTag;
using Khadamat_FileService.Infrastructure.Common.Persistence;
using Khadamat_FileService.Infrastructure.KhadamatFiles;
using Khadamat_FileService.Infrastructure.KhadamatFiles.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            return services;
        }

    }
}
