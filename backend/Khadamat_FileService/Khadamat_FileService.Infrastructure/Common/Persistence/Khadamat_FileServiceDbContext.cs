using Khadamat_FileService.Application.Common.FileContext;
using Khadamat_FileService.Application.Common.Interfaces;
using Khadamat_FileService.Domain.Common;
using Khadamat_FileService.Domain.Common.Events;
using Khadamat_FileService.Domain.FileAggregate;
using Khadamat_SellerPortal.Infrastructure.IntegrationEvents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Khadamat_FileService.Infrastructure.Common.Persistence
{
    public class Khadamat_FileServiceDbContext : DbContext, IUnitOfWork
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPublisher _publisher;

        public DbSet<KhadamatFile> KhadamatFiles { get; set; }
        public DbSet<OutboxIntegrationEvent> OutboxIntegrationEvents { get; set; }
        public DbSet<FileContextMetadata> FilesContextMetadata { get; set; }
        public Khadamat_FileServiceDbContext(DbContextOptions<Khadamat_FileServiceDbContext> options, IHttpContextAccessor httpContextAccessor, IPublisher publisher) : base(options)
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
            await PublishDomainEvents(domainEvents);
            await base.SaveChangesAsync(cancellationToken);
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
