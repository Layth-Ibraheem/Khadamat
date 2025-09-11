using Khadamat_FileService.Application.Common.FileContext;
using Khadamat_FileService.Application.Common.Interfaces;
using Khadamat_FileService.Domain.Common.Interfaces;
using Khadamat_FileService.Domain.FileAggregate;
using Khadamat_SharedKernal.Khadamat_SellerPortal;
using MediatR;
using Microsoft.AspNetCore.StaticFiles;

namespace Khadamat_FileService.Application.KhadamatFiles.IntegrationEvents
{
    public class EducationFileUploadedIntegrationEventHandler : INotificationHandler<EducationFileUploadedIntegrationEvent>
    {
        private readonly IFilesManagerService _filesManagerService;
        private readonly IFileRepository _fileRepository;
        private readonly IEntityTagGenerator _entityTagGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilesContextMetadataRepo _filesContextMetadataRepo;
        public EducationFileUploadedIntegrationEventHandler(IFileRepository fileRepository, IFilesManagerService filesManagerService, IEntityTagGenerator entityTagGenerator, IUnitOfWork unitOfWork, IFilesContextMetadataRepo filesContextMetadataRepo)
        {
            _fileRepository = fileRepository;
            _filesManagerService = filesManagerService;
            _entityTagGenerator = entityTagGenerator;
            _unitOfWork = unitOfWork;
            _filesContextMetadataRepo = filesContextMetadataRepo;
        }

        public async Task Handle(EducationFileUploadedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var fileStreamRes = await _filesManagerService.GetFile(notification.TempPath);
            if (fileStreamRes != null)
            {
                string fileName = Path.GetFileName(notification.TempPath);
                // Upload the file using the Stream and original file name
                var uploadFileResult = await _filesManagerService.UploadEducationFile(
                    fileStreamRes.FileStream,                     // pass the stream directly
                    fileName,     // file name
                    notification.SellerId,
                    notification.Institution,
                    notification.FieldOfStudy
                );

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
                    KhadamatFileReferenceType.SellerEducationCertificate
                );

                fileStreamRes.FileStream.Dispose();

                khadamatFile.UpdateFileMetadata(storingResult.FullPath, storingResult.StoredFileName, _entityTagGenerator);
                var fileContextMetadata = new FileContextMetadata(notification.SellerId, notification.EducationId, null, null, khadamatFile.Path);
                await _fileRepository.AddFile(khadamatFile);
                await _filesContextMetadataRepo.AddAsync(fileContextMetadata);
                await _unitOfWork.CommitChangesAsync();
            }
            else
            {
                // log the error that the provided temp path has problems
            }

        }
    }
}
