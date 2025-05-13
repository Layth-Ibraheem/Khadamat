using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Contracts.Users
{
    public record AuthenticationResponse(int Id, string UserName, string Email, int Roles, bool IsActive, string Token);
}
