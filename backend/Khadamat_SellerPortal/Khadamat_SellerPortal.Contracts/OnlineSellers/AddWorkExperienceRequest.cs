using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.OnlineSellers
{
    public record AddWorkExperienceRequest(string CompanyName, DateTime Start, DateTime? End, bool UntilNow, string Position, string Field, List<AddCertificateRequest> Certificates);
}
