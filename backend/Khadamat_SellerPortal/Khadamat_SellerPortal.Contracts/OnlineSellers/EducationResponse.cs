using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.OnlineSellers
{
    public record EducationResponse(int id, string Institution, string fieldOfStudy, string degree, DateTime start, DateTime? end, CertificateResponse certificate, bool isGraduated);
}
