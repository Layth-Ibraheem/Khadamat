using ErrorOr;
using Khadamat_FileService.Domain.FileAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Khadamat_FileService.Application.Common.Interfaces
{
    public interface IFilesManagerService
    {
        Task<ErrorOr<(string storedFileName, string fullPath)>> UploadFile(IFormFile file,
            string originalFileName,
            string nationalNo,
            KhadamatFileReferenceType fileType,
            IDictionary<string, string> metdata);
        Task<FileStreamResult?> GetFile(string path);
        Task DeleteFile(string path);
        Task<string> UpdateFile(string path, IFormFile newFile);
    }
}
