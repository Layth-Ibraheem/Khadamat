using ErrorOr;
using Khadamat_UserManagement.Domain.RegisterApplicationAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.RegisterApplications.Queries.GetAllRegisterApplications
{
    public record GetAllRegisterApplicationsQuery(ApplicationStatus? Status = null, int? UserId = null) : IRequest<IReadOnlyList<RegisterApplication>>;
}
