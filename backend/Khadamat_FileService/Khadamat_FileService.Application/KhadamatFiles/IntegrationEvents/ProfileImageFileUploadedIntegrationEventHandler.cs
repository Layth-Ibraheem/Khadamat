using Khadamat_FileService.Application.Common.FileContext;
using Khadamat_FileService.Application.Common.Interfaces;
using Khadamat_FileService.Domain.Common.Interfaces;
using Khadamat_FileService.Domain.FileAggregate;
using Khadamat_SharedKernal.Khadamat_SellerPortal;
using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Application.KhadamatFiles.IntegrationEvents
{
    public class ProfileImageFileUploadedIntegrationEventHandler : INotificationHandler<ProfileImageCreatedIntegrationEvent>
    {
        private readonly IFilesManagerService _filesManagerService;
        private readonly IEntityTagGenerator _entityTagGenerator;
        private readonly IFileRepository _fileRepository;
        private readonly IFilesContextMetadataRepo _filesContextMetadataRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileImageFileUploadedIntegrationEventHandler(IFilesManagerService filesManagerService, IEntityTagGenerator entityTagGenerator, IFileRepository fileRepository, IFilesContextMetadataRepo filesContextMetadataRepo, IUnitOfWork unitOfWork)
        {
            _filesManagerService = filesManagerService;
            _entityTagGenerator = entityTagGenerator;
            _fileRepository = fileRepository;
            _filesContextMetadataRepo = filesContextMetadataRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ProfileImageCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var fileStreamRes = await _filesManagerService.GetFile(notification.TempPath);
            if (fileStreamRes != null)
            {
                string fileName = Path.GetFileName(notification.TempPath);
                var uploadFileResult = await _filesManagerService.UploadProfileImageFile(fileStreamRes.FileStream, fileName, notification.SellerId);
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

                var khadamatFile = new KhadamatFile(fileName, contentType, fileStreamRes.FileStream.Length, KhadamatFileReferenceType.SellerProfileImage);
                fileStreamRes.FileStream.Dispose();
                khadamatFile.UpdateFileMetadata(storingResult.FullPath, storingResult.StoredFileName, _entityTagGenerator);
                var fileContextMetadata = new FileContextMetadata(notification.SellerId, null, null, null, khadamatFile.Path);
                await _fileRepository.AddFile(khadamatFile);
                await _filesContextMetadataRepo.AddAsync(fileContextMetadata);
                await _unitOfWork.CommitChangesAsync();
            }
        }
    }
}
