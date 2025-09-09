using ErrorOr;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.Common.Interfcaes
{
    public interface IFileService
    {
        Task<ErrorOr<string>> UploadSellerWorkExperienceCertificate(IFormFile file, string nationalNo, string companyName);
        Task<ErrorOr<string>> UploadSellerEducationCertificate(IFormFile file, string nationalNo, string institution, string fieldOfStudy);
        Task<bool> DeleteTempFile(string path);
    }
}
