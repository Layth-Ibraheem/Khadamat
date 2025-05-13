using ErrorOr;
using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Application.Users.Common;
using Khadamat_UserManagement.Domain.Common.Interfaces;
using Khadamat_UserManagement.Domain.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Users.Commands.CreateNewUser
{
    public class CreateNewUserCommandHandler : IRequestHandler<CreateNewUserCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public CreateNewUserCommandHandler(IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
        {
            File.AppendAllText("p.txt", $"Username: {request.UserName}, Password: {request.Password}");

            if (await _userRepository.IsUserExistsByUserNameAsync(request.UserName))
            {
                return Errors.UserErrors.UserNameExists;
            }
            if (await _userRepository.IsUserExistsByEmailAsync(request.Email))
            {
                return Errors.UserErrors.EmailExists;
            }
            var user = new User(request.UserName, _passwordHasher.HashPassword(request.Password), request.Email, request.Roles, request.IsActive);
            user.Register();

            await _userRepository.AddNewUserAsync(user);
            await _unitOfWork.CommitChangesAsync();

            string token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
