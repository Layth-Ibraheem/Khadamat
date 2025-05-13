using Khadamat_FileService.Domain.FileAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Infrastructure.Common.Persistence
{
    public class Khadamat_FileServiceDbContext : DbContext
    {
        public DbSet<KhadamatFile> KhadamatFiles { get; set; }
        public Khadamat_FileServiceDbContext(DbContextOptions<Khadamat_FileServiceDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
