using Khadamat_UserManagement.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Users.Common
{
    public record AuthenticationResult(User LoginUser, string Token);
}
