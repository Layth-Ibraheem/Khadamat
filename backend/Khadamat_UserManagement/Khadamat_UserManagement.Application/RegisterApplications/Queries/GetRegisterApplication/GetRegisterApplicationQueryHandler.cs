using ErrorOr;
using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Domain.RegisterApplicationAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.RegisterApplications.Queries.GetRegisterApplication
{
    public class GetRegisterApplicationQueryHandler : IRequestHandler<GetRegisterApplicationQuery, ErrorOr<RegisterApplication>>
    {
        private readonly IRegisterApplicationRepository _repository;
        public GetRegisterApplicationQueryHandler(IRegisterApplicationRepository repository)
        {
            _repository = repository;
        }
        public async Task<ErrorOr<RegisterApplication>> Handle(GetRegisterApplicationQuery request, CancellationToken cancellationToken)
        {
            var application = await _repository.GetApplicationByIdAsync(request.Id);
            if (application is null)
            {
                return Errors.RegisterApplicationErrors.NotFound;
            }
            return application;
        }
    }
}
