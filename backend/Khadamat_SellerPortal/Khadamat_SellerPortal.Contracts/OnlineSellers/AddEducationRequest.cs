using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.OnlineSellers
{
    public record AddEducationRequest(string institution, string fieldOfStudy, EducationDegree degree, bool isGraduated, DateTime start, DateTime end, AddCertificateRequest certificate);
}
