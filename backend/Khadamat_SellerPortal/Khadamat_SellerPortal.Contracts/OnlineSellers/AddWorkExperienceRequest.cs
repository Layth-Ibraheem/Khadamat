using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.OnlineSellers
{
    public record AddWorkExperienceRequest(string companyName, DateTime start, DateTime? end, bool untilNow, string position, string field, List<AddCertificateRequest> certificates);
}
