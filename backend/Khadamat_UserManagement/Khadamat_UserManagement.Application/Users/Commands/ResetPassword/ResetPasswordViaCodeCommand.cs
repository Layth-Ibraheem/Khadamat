using ErrorOr;
using MediatR;

namespace Khadamat_UserManagement.Application.Users.Commands.ResetPassword
{
    public record ResetPasswordViaCodeCommand(string Email, string Code, string NewPassword) : IRequest<ErrorOr<Success>>;
}
