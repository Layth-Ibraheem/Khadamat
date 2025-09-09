using Khadamat_FileService.Application.Common.FileContext;
using Khadamat_FileService.Application.Common.Interfaces;
using Khadamat_FileService.Domain.Common.Interfaces;
using Khadamat_FileService.Domain.FileAggregate;
using Khadamat_SharedKernal.Khadamat_SellerPortal;
using MediatR;
using Microsoft.AspNetCore.StaticFiles;

namespace Khadamat_FileService.Application.KhadamatFiles.IntegrationEvents
{
    public class WorkExperienceUploadedIntegrationEventHandler : INotificationHandler<WorkExperienceFileUploadedIntegrationEvent>
    {
        private readonly IFilesManagerService _filesManagerService;
        private readonly IFileRepository _fileRepository;
        private readonly IEntityTagGenerator _entityTagGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilesContextMetadataRepo _filesContextMetadataRepo;

        public WorkExperienceUploadedIntegrationEventHandler(IFilesManagerService filesManagerService, IFileRepository fileRepository, IEntityTagGenerator entityTagGenerator, IUnitOfWork unitOfWork, IFilesContextMetadataRepo filesContextMetadataRepo)
        {
            _filesManagerService = filesManagerService;
            _fileRepository = fileRepository;
            _entityTagGenerator = entityTagGenerator;
            _unitOfWork = unitOfWork;
            _filesContextMetadataRepo = filesContextMetadataRepo;
        }

        public async Task Handle(WorkExperienceFileUploadedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var fileStreamRes = await _filesManagerService.GetFile(notification.TempPath);
            if(fileStreamRes != null)
            {
                string fileName = Path.GetFileName(notification.TempPath);
                var uploadFileResult = await _filesManagerService.UploadWorkExperienceFile(
                    fileStreamRes.FileStream,
                    fileName,
                    notification.NationalNo,
                    notification.CompanyName,
                    notification.Position);

                if (uploadFileResult.IsError)
                {
                    // log the error
                    return;
                }

                var storingResult = uploadFileResult.Value;

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(notification.TempPath, out var contentType))
                {
                    contentType = "application/octet-stream"; // fallback
                }

                var khadamatFile = new KhadamatFile(
                    fileName,
                    contentType,
                    fileStreamRes.FileStream.Length,
                    KhadamatFileReferenceType.SellerWorkExperienceCertificate
                );

                fileStreamRes.FileStream.Dispose();

                khadamatFile.UpdateFileMetadata(storingResult.FullPath, storingResult.StoredFileName, _entityTagGenerator);
                var fileContextMetadata = new FileContextMetadata(notification.NationalNo, null, null, notification.CompanyName, notification.Position, khadamatFile.Path);
                await _fileRepository.AddFile(khadamatFile);
                await _filesContextMetadataRepo.AddAsync(fileContextMetadata);
                await _unitOfWork.CommitChangesAsync();
            }
            else
            {
                // log the error
            }
        }
    }
}
