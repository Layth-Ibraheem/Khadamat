using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Khadamat_SellerPortal.Contracts.Certificates;

namespace Khadamat_SellerPortal.Contracts.WorkExperiences
{
    public record WorkExperienceResponse(
        int Id,
        string CompanyName,
        string Position,
        string Field,
        DateTime Start,
        DateTime? End,
        List<CertificateResponse> Certificates,
        bool UntilNow);
}
