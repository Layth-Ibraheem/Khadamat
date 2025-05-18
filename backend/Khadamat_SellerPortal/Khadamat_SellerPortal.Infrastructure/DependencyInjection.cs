using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Infrastructure.Common;
using Khadamat_SellerPortal.Infrastructure.Common.Persistence;
using Khadamat_SellerPortal.Infrastructure.OnlineSellers.Persistence;
using Khadamat_SellerPortal.Infrastructure.Sellers.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return services;
        }
    }
}
