using ErrorOr;
using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Domain.RegisterApplicationAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.RegisterApplications.Commands.CreateNewRegisterApplication
{
    public class CreateNewRegisterApplicationCommandHandler : IRequestHandler<CreateNewRegisterApplicationCommand, ErrorOr<RegisterApplication>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegisterApplicationRepository _applicationRepository;
        public CreateNewRegisterApplicationCommandHandler(IRegisterApplicationRepository applicationRepository, IUnitOfWork unitOfWork)
        {
            _applicationRepository = applicationRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<RegisterApplication>> Handle(CreateNewRegisterApplicationCommand request, CancellationToken cancellationToken)
        {
            if (await _applicationRepository.IsApplicationExists(request.Username))
            {
                return Errors.RegisterApplicationErrors.AlreadyExists;
            }
            var application = new RegisterApplication(request.Username, request.Password, request.Email);

            await _applicationRepository.AddRegisterApplicationAsync(application);
            await _unitOfWork.CommitChangesAsync();

            return application;
        }
    }
}
