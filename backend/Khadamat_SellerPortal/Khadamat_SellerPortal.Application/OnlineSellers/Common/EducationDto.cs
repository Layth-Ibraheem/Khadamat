using Khadamat_SellerPortal.Domain.Common.Entities.EducationEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.OnlineSellers.Common
{
    public record EducationDto(string Institution, string FieldOfStudy, EducationDegree Degree, DateTime Start, DateTime? End, CertificateDto EducationCertificate, bool IsGraduated = false);
}
