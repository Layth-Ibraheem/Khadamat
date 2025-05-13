using ErrorOr;
using Khadamat_UserManagement.Application.Users.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Users.Commands.CreateNewUser
{
    public record CreateNewUserCommand(string UserName, string Password, string Email, int Roles, bool IsActive) : IRequest<ErrorOr<AuthenticationResult>>;
}
