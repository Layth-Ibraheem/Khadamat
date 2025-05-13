using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Contracts.RegisterApplications
{
    public record RegisterApplicationResponse(int Id, string Username, string Email, ApplicationStatus Status);
}
