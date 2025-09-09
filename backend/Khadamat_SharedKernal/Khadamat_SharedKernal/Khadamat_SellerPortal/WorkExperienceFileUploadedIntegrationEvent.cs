using Khadamat_SharedKernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SharedKernal.Khadamat_SellerPortal
{
    public record WorkExperienceFileUploadedIntegrationEvent(int WorkExperienceId, int CertificateId, int SellerId, string CompanyName, string Position, string TempPath) : IIntegrationEvent;
}