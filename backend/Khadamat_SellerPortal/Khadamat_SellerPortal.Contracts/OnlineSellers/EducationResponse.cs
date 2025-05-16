using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.OnlineSellers
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
