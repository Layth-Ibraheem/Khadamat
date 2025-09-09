using Khadamat_SharedKernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SharedKernal.Khadamat_FilesService
{
    public record WorkExperienceFileSavedIntegrationEvent(string FullPath, int SellerId, int WorkExperienceId, int CertificateId, int FileId) : IIntegrationEvent;
}
