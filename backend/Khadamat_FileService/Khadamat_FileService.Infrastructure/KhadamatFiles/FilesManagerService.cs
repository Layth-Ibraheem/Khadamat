using ErrorOr;
using Khadamat_FileService.Application.Common.Interfaces;
using Khadamat_FileService.Application.KhadamatFiles.Common;
using Khadamat_FileService.Domain.FileAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IO.Pipes;

namespace Khadamat_FileService.Infrastructure.KhadamatFiles
{
    public class FilesManagerService : IFilesManagerService
    {
        private const string BaseStorage = @"D:\Khadamat Seller Files";

        public Task CreateSellerFolderStructureAsync(string nationalNo)
        {
            var sellerFolder = Path.Combine(BaseStorage, nationalNo);
            var workExperiencesFolder = Path.Combine(sellerFolder, "WorkExperiences");
            var educationFolder = Path.Combine(sellerFolder, "Education");

            Directory.CreateDirectory(workExperiencesFolder);
            Directory.CreateDirectory(educationFolder);


            return Task.CompletedTask;
        }

        public async Task DeleteFile(string path)
        {
            await Task.CompletedTask;
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty", nameof(path));
            }

            var fullPath = Path.Combine(BaseStorage, path);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("File not found", fullPath);
            }

            File.Delete(fullPath);
        }

        public async Task<FileStreamResult?> GetFile(string path)
        {
            await Task.CompletedTask;
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty", nameof(path));
            }

            var fullPath = Path.Combine(BaseStorage, path);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("File not found", fullPath);
            }

            // Open the stream with sharing options that avoid file locking issues
            var fileStream = new FileStream(
                fullPath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite | FileShare.Delete);

            var contentType = GetContentType(Path.GetExtension(fullPath));
            var fileName = Path.GetFileName(fullPath);

            return new FileStreamResult(fileStream, contentType)
            {
                FileDownloadName = fileName
            };
        }

        public async Task<string> UpdateFile(string path, IFormFile newFile)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty", nameof(path));
            }

            if (newFile == null || newFile.Length == 0)
            {
                throw new ArgumentException("File is empty or null", nameof(newFile));
            }

            var fullPath = Path.Combine(BaseStorage, path);

            // Delete the existing file if it exists
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            // Create directory if it doesn't exist
            var directoryPath = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Save the new file
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await newFile.CopyToAsync(stream);
            }

            return path;
        }

        
        public async Task<ErrorOr<UploadFileResult>> UploadEducationFile(Stream fileStream, string fileName, int sellerId, string institution, string fieldOfStudy)
        {
            var fileExtension = Path.GetExtension(fileName);
            var guid = Guid.NewGuid().ToString();
            var storedFileName = $"{guid}{fileExtension}";
            var relativePath = $@"{sellerId}\Educations\{institution}-{fieldOfStudy}\Certificate-{guid}\{storedFileName}";
            var fullPath = Path.Combine(BaseStorage, relativePath);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

                // Write safely (exclusive lock while writing)
                using (var destinationStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await fileStream.CopyToAsync(destinationStream);
                }


                return new UploadFileResult(fullPath, storedFileName);
            }
            catch (Exception e)
            {
                return Error.Unexpected("File.Unexpected", e.Message);
            }
        }
        
        public async Task<ErrorOr<UploadFileResult>> UploadWorkExperienceFile(Stream fileStream, string fileName, int sellerId, string companyName, string position)
        {
            var fileExtension = Path.GetExtension(fileName);
            var guid = Guid.NewGuid().ToString();
            var storedFileName = $"{guid}{fileExtension}";
            var relativePath = $@"{sellerId}\Educations\{companyName}-{position}\Certificate-{guid}\{storedFileName}";
            var fullPath = Path.Combine(BaseStorage, relativePath);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

                // Write safely (exclusive lock while writing)
                using (var destinationStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await fileStream.CopyToAsync(destinationStream);
                }


                return new UploadFileResult(fullPath, storedFileName);
            }
            catch (Exception e)
            {
                return Error.Unexpected("File.Unexpected", e.Message);
            }
        }


        private string GetContentType(string fileExtension)
        {
            // You can expand this dictionary with more MIME types as needed
            var mimeTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".zip", "application/zip"}
            };

            return mimeTypes.TryGetValue(fileExtension, out var mimeType)
                ? mimeType
                : "application/octet-stream";
        }


        //public async Task<ErrorOr<(string storedFileName, string fullPath)>> UploadFile(
        //    IFormFile file,
        //    string nationalNo,
        //    KhadamatFileReferenceType fileType,
        //    IDictionary<string, string> metdata)
        //{
        //    try
        //    {
        //        if (file == null || file.Length == 0)
        //        {
        //            throw new ArgumentException("File is empty or null", nameof(file));
        //        }
        //        var (storedFileName, fullPath) = GeneratePaths(nationalNo, fileType, metdata, file.Name);

        //        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));


        //        // Save the file
        //        using (var stream = new FileStream(fullPath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        return (storedFileName, fullPath);
        //    }
        //    catch (Exception e)
        //    {
        //        return Error.Failure("CannotCreateFile", e.Message);
        //    }
        //}
        //private (string storedFileName, string fullPath) GeneratePaths(
        //string nationalNo,
        //KhadamatFileReferenceType fileType,
        //IDictionary<string, string> metadata,
        //string originalFileName)
        //{
        //    var fileExtension = Path.GetExtension(originalFileName);
        //    var storedFileName = "";
        //    string relativePath = "";
        //    if (fileType == KhadamatFileReferenceType.SellerEducationCertificate)
        //    {
        //        var guid = Guid.NewGuid().ToString();
        //        storedFileName = $"{guid}{fileExtension}";
        //        relativePath = @$"{nationalNo}\educations\{metadata["institution"]}-{metadata["fieldOfStudy"]}\certificate-{guid}\{storedFileName}";
        //    }
        //    if (fileType == KhadamatFileReferenceType.SellerWorkExperienceCertificate)
        //    {
        //        var guid = Guid.NewGuid().ToString();
        //        storedFileName = $"{guid}{fileExtension}";
        //        relativePath = $@"{nationalNo}\work-experiences\{metadata["company"]}\certificate-{guid}\{storedFileName}";
        //    }
        //    if (fileType == KhadamatFileReferenceType.SellerProfileImage)
        //    {
        //        var guid = Guid.NewGuid().ToString();
        //        storedFileName = $"{guid}{fileExtension}";
        //        relativePath = @$"{nationalNo}\profile-{guid}\{storedFileName}";
        //    }

        //    var fullPath = Path.Combine(BaseStorage, relativePath);
        //    return (storedFileName, fullPath);
        //}


        //private string GetSafeFileName(string originalFileName)
        //{
        //    string safeName = "";
        //    safeName = originalFileName.Replace(" ", "-");
        //}
    }
}
