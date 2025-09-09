using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.Certificates
{
    public record AddCertificateRequest(string FilePath, string Description, IFormFile File);
}
