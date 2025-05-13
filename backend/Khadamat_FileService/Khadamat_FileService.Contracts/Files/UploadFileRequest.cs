using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Contracts.Files
{
    public record UploadFileRequest(
        IFormFile file,
        string nationalNo,
        KhadamatFileReference referenceType,
        string? institution = null,
        string? fieldOfStudy = null,
        string? company = null);
}
