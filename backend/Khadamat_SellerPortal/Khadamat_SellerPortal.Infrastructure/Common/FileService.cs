using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.Common
{
    public class FileService : IFileService
    {
        private const string BaseStorage = @"D:\Khadamat\SellersFiles";
        public FileService()
        {
        }

        public async Task<bool> DeleteTempFile(string path)
        {
            await Task.CompletedTask;
            if(File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }

        public async Task<ErrorOr<string>> UploadSellerEducationCertificate(IFormFile file, string nationalNo, string institution, string fieldOfStudy)
        {
            try
            {
                var certificateGuid = Guid.NewGuid().ToString();
                var directoryPath = Path.Combine(BaseStorage, nationalNo, "educations", $"{institution}-{fieldOfStudy}", $"certificate-{certificateGuid}");

                Directory.CreateDirectory(directoryPath);

                var uniqueFileName = $"{certificateGuid}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(directoryPath, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Path.Combine(directoryPath, uniqueFileName).Replace("\\", "/");
            }
            catch (Exception e)
            {

                return Error.Failure("File.CannotCreateFile", e.Message);
            }
        }

        public async Task<ErrorOr<string>> UploadSellerWorkExperienceCertificate(IFormFile file, string nationalNo, string companyName)
        {
            try
            {
                var certificateGuid = Guid.NewGuid().ToString();
                var directoryPath = Path.Combine(BaseStorage, nationalNo, "work-experiences", $"{companyName}", $"certificate-{certificateGuid}");
                Directory.CreateDirectory(directoryPath);

                var uniqueFileName = $"{certificateGuid}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(directoryPath, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Path.Combine(directoryPath, uniqueFileName);

            }
            catch (Exception e)
            {
                return Error.Failure("File.CannotCreateFile", e.Message);
            }
        }
    }
}
