using MediatR;
using Khadamat_SharedKernal.Common;
using System.Text.Json;
using Khadamat_FileService.Domain.FileAggregate.Events;
using Khadamat_FileService.Infrastructure.Common.Persistence;
using Khadamat_SharedKernal.Khadamat_FilesService;
using Khadamat_FileService.Application.Common.FileContext;
using System.Diagnostics;

#nullable disable

namespace Khadamat_SellerPortal.Infrastructure.IntegrationEvents.OutboxWriter
{
    public class OuboxWriterEventHandler : INotificationHandler<EducationFileSavedDomainEvent>, INotificationHandler<WorkExperienceFileSavedDomainEvent>
    {
        private readonly Khadamat_FileServiceDbContext _context;
        private readonly IFilesContextMetadataRepo _metadataRepo;
        public OuboxWriterEventHandler(Khadamat_FileServiceDbContext context, IFilesContextMetadataRepo metadataRepo)
        {
            _context = context;
            _metadataRepo = metadataRepo;
        }

        public async Task Handle(EducationFileSavedDomainEvent notification, CancellationToken cancellationToken)
        {
            var fileContextMetadata = await _metadataRepo.GetByPathAsync(notification.FullPath);
            var integrationEvent = new EducationFileSavedIntegrationEvent(
                notification.FullPath,
                fileContextMetadata.SellerId,
                fileContextMetadata.EducationId.Value,
                notification.FileId);

            await AddOutboxIntegrationEventAsync(integrationEvent);
        }

        public async Task Handle(WorkExperienceFileSavedDomainEvent notification, CancellationToken cancellationToken)
        {
            var fileContextMetadata = await _metadataRepo.GetByPathAsync(notification.FullPath);
            var integrationEvent = new WorkExperienceFileSavedIntegrationEvent(
                notification.FullPath,
                fileContextMetadata.SellerId,
                fileContextMetadata.WorkExperienceId.Value,
                fileContextMetadata.CertificateId.Value,
                notification.FileId);

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
