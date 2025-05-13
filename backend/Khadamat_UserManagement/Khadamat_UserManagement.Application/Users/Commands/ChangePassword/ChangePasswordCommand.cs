using ErrorOr;
using Khadamat_UserManagement.Application.Users.Common;
using MediatR;

namespace Khadamat_UserManagement.Application.Users.Commands.ChangePassword
{
    public record ChangePasswordCommand(string Password, string NewPassword) : IRequest<ErrorOr<Success>>;
}
