using ErrorOr;
using Khadamat_UserManagement.Application.Users.Common;
using Khadamat_UserManagement.Domain.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Users.Queries.Login
{
    public record LoginQuery(string UserName, string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
