using ErrorOr;
using Khadamat_FileService.Application.Common.Interfaces;
using Khadamat_FileService.Application.KhadamatFiles.Common;
using MediatR;

namespace Khadamat_FileService.Application.KhadamatFiles.Queries.DownloadFile
{
    public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, ErrorOr<DownloadFileDto>>
    {
        private readonly IFilesManagerService _filesManagerService;
        private readonly IFileRepository _fileRepository;

        public DownloadFileQueryHandler(IFilesManagerService filesManagerService, IFileRepository fileRepository)
        {
            _filesManagerService = filesManagerService;
            _fileRepository = fileRepository;
        }

        public async Task<ErrorOr<DownloadFileDto>> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            var file = await _filesManagerService.GetFile(request.Path);
            if (file == null)
            {
                return Error.NotFound("File.NotExist", "There is no file in the provided path");
            }
            var PathToDatabase = request.Path;
            var khadamatFile = await _fileRepository.GetFileByPath(PathToDatabase);
            return new DownloadFileDto(khadamatFile!.Id, file);
        }
    }
}
