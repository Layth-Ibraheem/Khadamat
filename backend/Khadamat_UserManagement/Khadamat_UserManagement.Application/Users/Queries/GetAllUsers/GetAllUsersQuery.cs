using Khadamat_UserManagement.Application.Common.Authorization;
using Khadamat_UserManagement.Domain.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Users.Queries.GetAllUsers
{
    [Authorization(UserRole.ListUsers)]
    public record GetAllUsersQuery() : IRequest<List<User>>;
}
