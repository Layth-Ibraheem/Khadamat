using ErrorOr;
using Khadamat_UserManagement.Application.Common.Interfaces;
using MediatR;
using Khadamat_UserManagement.Application.RegisterApplications.Queries.GetRegisterApplication;

namespace Khadamat_UserManagement.Application.RegisterApplications.Commands.ApproveApplication
{
    public class ApproveApplicationCommandHandler : IRequestHandler<ApproveApplicationCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegisterApplicationRepository _applicationRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ISender _mediator;
        public ApproveApplicationCommandHandler(IRegisterApplicationRepository applicationRepository, IUnitOfWork unitOfWork, ICurrentUserProvider currentUserProvider, ISender mediator)
        {
            _applicationRepository = applicationRepository;
            _unitOfWork = unitOfWork;
            _currentUserProvider = currentUserProvider;
            _mediator = mediator;
        }
        public async Task<ErrorOr<Success>> Handle(ApproveApplicationCommand request, CancellationToken cancellationToken)
        {
            var query = new GetRegisterApplicationQuery(request.Id);
            var getApplicationResult = await _mediator.Send(query);
            if (getApplicationResult.IsError)
            {
                return getApplicationResult.FirstError;
            }
            var application = getApplicationResult.Value;

            var user = _currentUserProvider.GetCurrentUser().GetActualUser();

            var approvalResult = application.Approve(user);
            if (approvalResult.IsError)
            {
                return approvalResult.FirstError;
            }

            application.Roles = request.Roles;

            await _applicationRepository.ApproveRegisterApplicationAsync(application.Id);
            await _unitOfWork.CommitChangesAsync();

            return Result.Success;
        }
    }
}
