using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Khadamat_SellerPortal.Contracts.Certificates;

namespace Khadamat_SellerPortal.Contracts.Educations
{
    public record EducationResponse(
        int Id,
        string Institution,
        string FieldOfStudy,
        string Degree,
        DateTime Start,
        DateTime? End,
        CertificateResponse Certificate,
        bool IsGraduated);
}
