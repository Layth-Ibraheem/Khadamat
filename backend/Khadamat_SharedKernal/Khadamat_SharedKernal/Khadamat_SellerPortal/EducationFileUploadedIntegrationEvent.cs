using Khadamat_SharedKernal.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SharedKernal.Khadamat_SellerPortal
{
    public record EducationFileUploadedIntegrationEvent(int EducationId, int SellerId, string Institution, string FieldOfStudy, string TempPath) : IIntegrationEvent;
}
