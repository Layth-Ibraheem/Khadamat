using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Contracts.RegisterApplications
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ApplicationStatus
    {
        Rejected = 0,
        Approved = 1, 
        Draft = 2,
    }
}
