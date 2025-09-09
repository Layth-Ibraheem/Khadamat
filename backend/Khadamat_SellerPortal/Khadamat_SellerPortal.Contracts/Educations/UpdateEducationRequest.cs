using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.Educations
{
    public record UpdateEducationRequest(string Institution,
        string FieldOfStudy,
        EducationDegree Degree,
        bool IsGraduated,
        DateTime Start,
        DateTime? End);
}
