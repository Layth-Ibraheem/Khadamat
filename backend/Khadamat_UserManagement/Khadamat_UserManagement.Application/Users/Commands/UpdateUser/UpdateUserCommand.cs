using ErrorOr;
using Khadamat_UserManagement.Application.Common.Authorization;
using Khadamat_UserManagement.Domain.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Users.Commands.UpdateUser
{
    [Authorization(UserRole.UpdateUser)]
    public record UpdateUserCommand(string UserName, string Email, int Roles, bool IsActive) : IRequest<ErrorOr<User>>;
}
