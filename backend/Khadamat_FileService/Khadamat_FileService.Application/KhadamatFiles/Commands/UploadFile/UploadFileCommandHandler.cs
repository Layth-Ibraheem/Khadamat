using ErrorOr;
using Khadamat_FileService.Application.Common.Interfaces;
using Khadamat_FileService.Domain.Common.Interfaces;
using Khadamat_FileService.Domain.FileAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Application.KhadamatFiles.Commands.UploadFile
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, ErrorOr<string>>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFilesManagerService _storeFilesService;
        private readonly IEntityTagGenerator _entityTagGenerator;

        public UploadFileCommandHandler(IFilesManagerService storeFilesService, IFileRepository fileRepository, IEntityTagGenerator entityTagGenerator)
        {
            _storeFilesService = storeFilesService;
            _fileRepository = fileRepository;
            _entityTagGenerator = entityTagGenerator;
        }

        public async Task<ErrorOr<string>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var khadamatFile = new KhadamatFile(request.File.FileName, request.File.ContentType, request.File.Length);
            var getPathRes = await _storeFilesService.UploadFile(request.File, request.File.FileName, request.NationalNo, request.ReferenceType, request.Metdata);
            if (getPathRes.IsError)
            {
                return getPathRes.FirstError;
            }
            var path = getPathRes.Value.fullPath;
            var storedFileName = getPathRes.Value.storedFileName;
            khadamatFile.UpdateFileMetadata(path, storedFileName, _entityTagGenerator);

            await _fileRepository.AddFile(khadamatFile);
            return path;
        }
    }
}
