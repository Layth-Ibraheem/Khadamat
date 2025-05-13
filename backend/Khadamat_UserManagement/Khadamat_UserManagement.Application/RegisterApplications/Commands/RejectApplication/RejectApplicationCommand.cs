using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.RegisterApplications.Commands.RejectApplication
{
    public record RejectApplicationCommand(int Id, string RejectionReason) : IRequest<ErrorOr<Success>>;
}
