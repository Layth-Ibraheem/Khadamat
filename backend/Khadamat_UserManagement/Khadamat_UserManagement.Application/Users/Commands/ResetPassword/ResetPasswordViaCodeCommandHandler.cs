using ErrorOr;
using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Domain.Common.Interfaces;
using Khadamat_UserManagement.Domain.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Users.Commands.ResetPassword
{
    public class ResetPasswordViaCodeCommandHandler : IRequestHandler<ResetPasswordViaCodeCommand, ErrorOr<Success>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordResetCodeProvider _passwordResetCodeProvider;
        private readonly IPasswordHasher _passwordHasher;
        public ResetPasswordViaCodeCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordResetCodeProvider passwordResetCodeProvider, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordResetCodeProvider = passwordResetCodeProvider;
            _passwordHasher = passwordHasher;
        }
        public async Task<ErrorOr<Success>> Handle(ResetPasswordViaCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user is null)
            {
                return Errors.UserErrors.NotFound;
            }

            if (!_passwordResetCodeProvider.ValidateResetCode(user.Email, request.Code))
            {
                return Errors.UserErrors.InvalidResetToken;
            }

            user.ResetPassword(request.NewPassword, _passwordHasher);

            await _userRepository.UpdateUserAsync(user);
            await _unitOfWork.CommitChangesAsync();

            return Result.Success;

        }
    }
}
