using ErrorOr;
using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Application.RegisterApplications.Queries.GetRegisterApplication;
using MediatR;

namespace Khadamat_UserManagement.Application.RegisterApplications.Commands.RejectApplication
{
    public class RejectApplicationCommandHandler : IRequestHandler<RejectApplicationCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegisterApplicationRepository _repository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ISender _mediator;
        public RejectApplicationCommandHandler(ICurrentUserProvider currentUserProvider, IRegisterApplicationRepository repository, IUnitOfWork unitOfWork, ISender mediator)
        {
            _currentUserProvider = currentUserProvider;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<ErrorOr<Success>> Handle(RejectApplicationCommand request, CancellationToken cancellationToken)
        {
            var query = new GetRegisterApplicationQuery(request.Id);
            var getApplicationResult = await _mediator.Send(query);
            if (getApplicationResult.IsError)
            {
                return getApplicationResult.FirstError;
            }
            var application = getApplicationResult.Value;

            var user = _currentUserProvider.GetCurrentUser().GetActualUser();

            var rejectionResult = application.Reject(user, request.RejectionReason);
            if (rejectionResult.IsError)
            {
                return rejectionResult.FirstError;
            }

            await _repository.UpdateRegisterApplicationAsync(application);
            await _unitOfWork.CommitChangesAsync();

            return Result.Success;
        }
    }
}
