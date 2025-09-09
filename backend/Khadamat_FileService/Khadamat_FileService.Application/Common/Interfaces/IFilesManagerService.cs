using ErrorOr;
using Khadamat_FileService.Application.KhadamatFiles.Common;
using Khadamat_FileService.Domain.FileAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Khadamat_FileService.Application.Common.Interfaces
{
    public interface IFilesManagerService
    {
        Task<ErrorOr<(string storedFileName, string fullPath)>> UploadFile(IFormFile file,
            string nationalNo,
            KhadamatFileReferenceType fileType,
            IDictionary<string, string> metdata);
        Task<FileStreamResult?> GetFile(string path);
        Task DeleteFile(string path);
        Task<string> UpdateFile(string path, IFormFile newFile);
        Task CreateSellerFolderStructureAsync(string nationalNo);
        Task<ErrorOr<UploadFileResult>> UploadEducationFile(Stream file, string fileName, string nationalNo, string institution, string fieldOfStudy);
        Task<ErrorOr<UploadFileResult>> UploadWorkExperienceFile(Stream file, string fileName, string nationalNo, string companyName, string position);
    }
}
