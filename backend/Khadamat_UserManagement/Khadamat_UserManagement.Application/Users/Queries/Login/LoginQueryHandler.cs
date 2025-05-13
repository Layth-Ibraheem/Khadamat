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

namespace Khadamat_UserManagement.Application.Users.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUnitOfWork _unitOfWork;
        public LoginQueryHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUserNameAsync(request.UserName);
            if (user is null)
            {
                return Error.NotFound("User.NotFound", "There is no user with such username or password");
            }
            var loginResult = user.Login(request.UserName, request.Password, _passwordHasher);

            if (loginResult.IsError)
            {
                return loginResult.FirstError;
            }

            string token = _jwtTokenGenerator.GenerateToken(user);
            await _unitOfWork.CommitChangesAsync();
            return new AuthenticationResult(user, token);

        }
    }
}
