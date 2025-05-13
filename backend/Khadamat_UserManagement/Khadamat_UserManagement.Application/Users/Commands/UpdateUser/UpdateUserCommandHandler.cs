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

namespace Khadamat_UserManagement.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.IsUserExistsByEmailAsync(request.Email))
            {
                return Errors.UserErrors.EmailExists;
            }
            var user = await _userRepository.GetUserByUserNameAsync(request.UserName);
            if (user is null)
            {
                return Errors.UserErrors.NotFound;
            }

            user.Update(request.Email, request.IsActive, request.Roles);

            await _userRepository.UpdateUserAsync(user);
            await _unitOfWork.CommitChangesAsync();

            return user;
        }
    }
}
