using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.RegisterApplications.Commands.ApproveApplication
{
    public record ApproveApplicationCommand(int Id, int Roles) : IRequest<ErrorOr<Success>>;
}
