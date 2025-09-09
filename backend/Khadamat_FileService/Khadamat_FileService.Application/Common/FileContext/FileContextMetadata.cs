using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Application.Common.FileContext
{
    public record FileContextMetadata(string NationalNo, string? Institution, string? FieldOfStudy, string? CompanyName, string? Position, string FilePath);
}
