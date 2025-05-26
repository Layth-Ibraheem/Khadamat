using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Khadamat_SellerPortal.Contracts.Certificates;

namespace Khadamat_SellerPortal.Contracts.Educations
{
    public record AddEducationRequest(
        string Institution,
        string FieldOfStudy,
        EducationDegree Degree,
        bool IsGraduated,
        DateTime Start,
        DateTime End,
        AddCertificateRequest Certificate);
}
