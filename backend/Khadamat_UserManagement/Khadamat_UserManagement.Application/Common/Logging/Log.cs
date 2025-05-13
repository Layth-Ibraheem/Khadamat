using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Common.Logging
{
    public record Log(
        int Id,
        int? UserId,
        string? EventType = null,
        string? EventBody = null,
        string? RequestId = null,
        string? RequestPath = null,
        string? EventCriticality = null,
        string? Notes = null,
        string? Message = null,
        DateTime? TimeStamp = null,
        string? Level = null,
        string? Exception = null);
}
