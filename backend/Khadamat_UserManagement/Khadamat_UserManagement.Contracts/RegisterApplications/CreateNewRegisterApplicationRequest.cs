using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Contracts.RegisterApplications
{
    public record CreateNewRegisterApplicationRequest(string Username, string Email, string Password);
}
