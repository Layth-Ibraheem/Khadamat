using Khadamat_SellerPortal.Domain.SellerAggregate.Events;
using Khadamat_SellerPortal.Infrastructure.Common.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Khadamat_SharedKernal.Common;
using System.Text.Json;
using Khadamat_SharedKernal.Khadamat_SellerPortal;
namespace Khadamat_SellerPortal.Infrastructure.IntegrationEvents.OutboxWriter
{
    public class OuboxWriterEventHandler :
        INotificationHandler<SellerCreatedEvent>,
        INotificationHandler<EducationFileUploadedEvent>,
        INotificationHandler<WorkExperienceFileUploadedEvent>,
        INotificationHandler<ProfileImageCreatedDomainEvent>
    {
        private readonly Khadamat_SellerPortalDbContext _context;

        public OuboxWriterEventHandler(Khadamat_SellerPortalDbContext context)
        {
            _context = context;
        }

        public async Task Handle(SellerCreatedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new SellerCreatedIntegrationEvent(notification.SellerId);

            await AddOutboxIntegrationEventAsync(integrationEvent);
        }

        public async Task Handle(EducationFileUploadedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new EducationFileUploadedIntegrationEvent(
                notification.EducationId,
                notification.SellerId,
                notification.Institution,
                notification.FieldOfStudy,
                notification.TempPath);
            await AddOutboxIntegrationEventAsync(integrationEvent);
        }
        public async Task Handle(WorkExperienceFileUploadedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new WorkExperienceFileUploadedIntegrationEvent(
                notification.WorkExperienceId,
                notification.CertificateId,
                notification.SellerId,
                notification.CompanyName,
                notification.Position,
                notification.TempPath);

            await AddOutboxIntegrationEventAsync(integrationEvent);
        }
        public async Task Handle(ProfileImageCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new ProfileImageCreatedIntegrationEvent(notification.SellerId, notification.TempPath);
            await AddOutboxIntegrationEventAsync(integrationEvent);
        }

        public async Task AddOutboxIntegrationEventAsync(IIntegrationEvent integrationEvent)
        {
            await _context.OutboxIntegrationEvents.AddAsync(new OutboxIntegrationEvent(
                EventName: integrationEvent.GetType().Name,
                EventContent: JsonSerializer.Serialize(integrationEvent)));

            await _context.SaveChangesAsync();
        }

    }
}
