using ErrorOr;
using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Application.Common.Logging;
using Khadamat_UserManagement.Application.Users.Common;
using Khadamat_UserManagement.Domain.Common.Interfaces;
using Khadamat_UserManagement.Domain.UserAggregate;
using MediatR;

namespace Khadamat_UserManagement.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<Success>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ILogsCollector _logsCollector;
        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, ICurrentUserProvider currentUserProvider, ILogsCollector logsCollector)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _currentUserProvider = currentUserProvider;
            _logsCollector = logsCollector;
        }
        public async Task<ErrorOr<Success>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();
            var user = await _userRepository.GetUserByUserNameAsync(currentUser.UserName);
            if (user is null)
            {
                return Errors.UserErrors.NotFound;
            }
            var resetPasswordResult = user.ResetPassword(request.Password, request.NewPassword, _passwordHasher);
            if (resetPasswordResult.IsError)
            {
                return resetPasswordResult.FirstError;
            }
            await _userRepository.UpdateUserAsync(user);
            await _unitOfWork.CommitChangesAsync();

            return Result.Success;


        }
    }
}
