using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Domain.Common;
using Khadamat_UserManagement.Domain.Common.Events;
using Khadamat_UserManagement.Domain.RegisterApplicationAggregate;
using Khadamat_UserManagement.Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Khadamat_UserManagement.Infrastructure.Common.Persistence
{
    public class Khadamat_UserDbContext : DbContext, IUnitOfWork
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DbSet<User> Users { get; set; }
        public DbSet<RegisterApplication> RegisterApplications { get; set; }
        public Khadamat_UserDbContext(DbContextOptions<Khadamat_UserDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Used to commit the changes to the database, use it when querying data and a domain event needs to be published (event if there is nothing to commit to the database)
        /// </summary>
        /// <returns></returns>
        public async Task CommitChangesAsync()
        {
            var domainEvents = ChangeTracker.Entries<Entity>()
                .Select(e => e.Entity.PopDomainEvents())
                .SelectMany(d => d)
                .ToList();

            AddDomainEventsToOfflineProcessingQueu(domainEvents);


            await SaveChangesAsync();
        }

        private void AddDomainEventsToOfflineProcessingQueu(List<IDomainEvent> domainEvents)
        {
            var domainEventsQueu = _httpContextAccessor.HttpContext!.Items
                .TryGetValue("DomainEventsQueue", out var value)
                && value is Queue<IDomainEvent> existingDomainEvents ? existingDomainEvents : new Queue<IDomainEvent>();

            domainEvents.ForEach(domainEventsQueu.Enqueue);

            _httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueu;
        }
    }
}
