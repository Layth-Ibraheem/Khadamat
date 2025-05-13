using ErrorOr;
using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Domain.RegisterApplicationAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.RegisterApplications.Queries.GetAllRegisterApplications
{
    public class GetAllRegisterApplicationsQueryHandler : IRequestHandler<GetAllRegisterApplicationsQuery, IReadOnlyList<RegisterApplication>>
    {
        private readonly IRegisterApplicationRepository _repository;
        public GetAllRegisterApplicationsQueryHandler(IRegisterApplicationRepository repository)
        {
            _repository = repository;
        }
        public async Task<IReadOnlyList<RegisterApplication>> Handle(GetAllRegisterApplicationsQuery request, CancellationToken cancellationToken)
        {
            var applications = await _repository.GetAllApplicationsAsyc(request.Status, request.UserId);
            return applications;
        }
    }
}
