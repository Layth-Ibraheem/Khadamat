using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Domain.OfflineSellerAggregate;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.Common.Persistence
{
    public class Khadamat_SellerPortalDbContext : DbContext, IUnitOfWork
    {
        public DbSet<OnlineSeller> OnlineSellers { get; set; }
        public DbSet<OfflineSeller> OfflineSellers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public Khadamat_SellerPortalDbContext(DbContextOptions<Khadamat_SellerPortalDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public async Task CommitChangesAsync()
        {
            await SaveChangesAsync();
        }
    }
}
