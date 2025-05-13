using ErrorOr;
using MediatR;

namespace Khadamat_UserManagement.Application.RegisterApplications.Commands.UpdateApplication
{
    public record UpdateApplicationCommand(int Id, string Username, string Email, string Password, int Roles) : IRequest<ErrorOr<Success>>;
}
