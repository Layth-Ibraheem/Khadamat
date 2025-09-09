using Khadamat_SellerPortal.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.Common.Files
{
    public class DomainFileService : IDomainFilesService
    {
        public async Task<string> UploadFileToTempPath(IFormFile file)
        {
            string tempPath = Path.GetTempPath(); // C:\Users\ASUS\AppData\Local\Temp\
            string fullPath = Path.Combine(tempPath, file.FileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await file.CopyToAsync(fileStream);
            }
            return fullPath;
        }
    }
}
