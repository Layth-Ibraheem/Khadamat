using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Domain.Common.Events;
using Khadamat_SellerPortal.Domain.Common;
using Khadamat_SellerPortal.Domain.OfflineSellerAggregate;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Khadamat_SellerPortal.Infrastructure.IntegrationEvents;
using MediatR;
using System.Threading;

namespace Khadamat_SellerPortal.Infrastructure.Common.Persistence
{
    public class Khadamat_SellerPortalDbContext : DbContext, IUnitOfWork
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPublisher _publisher;

        public DbSet<OnlineSeller> OnlineSellers { get; set; }
        public DbSet<OfflineSeller> OfflineSellers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<OutboxIntegrationEvent> OutboxIntegrationEvents { get; set; }
        public Khadamat_SellerPortalDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor, IPublisher publisher) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _publisher = publisher;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public async Task CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEvents = ChangeTracker.Entries<Entity>()
           .Select(entry => entry.Entity.PopDomainEvents())
           .SelectMany(x => x)
           .ToList();

            if (IsUserWaitingOnline())
            {
                AddDomainEventsToOfflineProcessingQueue(domainEvents);
                await base.SaveChangesAsync(cancellationToken);
                return;
            }

            await base.SaveChangesAsync(cancellationToken);
            await PublishDomainEvents(domainEvents);
        }
        private bool IsUserWaitingOnline() => _httpContextAccessor.HttpContext is not null;
        private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
        private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
        {
            var events = _httpContextAccessor.HttpContext!.Items
                .TryGetValue("DomainEventsKey", out var value)
                && value is Queue<IDomainEvent> existingDomainEvents ? existingDomainEvents : new Queue<IDomainEvent>();

            domainEvents.ForEach(events.Enqueue);

            _httpContextAccessor.HttpContext!.Items["DomainEventsKey"] = events;
        }
    }
}
