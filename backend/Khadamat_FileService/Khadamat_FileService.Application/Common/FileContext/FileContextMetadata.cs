using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Application.Common.FileContext
{
    public record FileContextMetadata(int SellerId, int? EducationId, int? WorkExperienceId, int? CertificateId, string FilePath);
}
