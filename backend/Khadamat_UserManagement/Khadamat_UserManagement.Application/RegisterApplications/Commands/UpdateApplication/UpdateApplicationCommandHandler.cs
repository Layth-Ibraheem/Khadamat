using ErrorOr;
using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Application.RegisterApplications.Queries.GetRegisterApplication;
using MediatR;

namespace Khadamat_UserManagement.Application.RegisterApplications.Commands.UpdateApplication
{
    public class UpdateApplicationCommandHandler : IRequestHandler<UpdateApplicationCommand, ErrorOr<Success>>
    {
        private readonly IRegisterApplicationRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISender _mediator;
        public UpdateApplicationCommandHandler(IUnitOfWork unitOfWork, IRegisterApplicationRepository repository, ISender mediator)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
        {
            var query = new GetRegisterApplicationQuery(request.Id);
            var result = await _mediator.Send(query);
            if (result.IsError)
            {
                return result.FirstError;
            }

            var application = result.Value;
            application.Update(request.Username, request.Email, request.Password, request.Roles);

            await _repository.UpdateRegisterApplicationAsync(application);
            await _unitOfWork.CommitChangesAsync();
            return Result.Success;
        }
    }
}
