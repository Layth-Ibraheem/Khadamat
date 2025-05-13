using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.OnlineSellers.Common
{
    public record WorkExperienceDto(string CompanyName, DateTime Start, DateTime? End, string Position, string Field, List<CertificateDto> Certificates, bool UntilNow = false);
}
