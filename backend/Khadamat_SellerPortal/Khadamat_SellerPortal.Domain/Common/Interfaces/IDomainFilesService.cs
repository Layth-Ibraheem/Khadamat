using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.Common.Interfaces
{
    public interface IDomainFilesService
    {
        Task<string> UploadFileToTempPath(IFormFile file);
    }
}
