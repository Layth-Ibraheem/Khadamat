using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.OnlineSellers
{
    public record WorkExperienceResponse(int id, string companyName, string position, string field, DateTime start, DateTime? end, List<CertificateResponse> certificates, bool untilNow);
}
