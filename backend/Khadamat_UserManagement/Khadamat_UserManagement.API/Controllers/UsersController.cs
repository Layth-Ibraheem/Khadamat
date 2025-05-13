using ErrorOr;
using Khadamat_UserManagement.Application.Users.Commands.ChangePassword;
using Khadamat_UserManagement.Application.Users.Commands.CreateNewUser;
using Khadamat_UserManagement.Application.Users.Commands.ResetPassword;
using Khadamat_UserManagement.Application.Users.Commands.UpdateUser;
using Khadamat_UserManagement.Application.Users.Queries.GetAllUsers;
using Khadamat_UserManagement.Application.Users.Queries.Login;
using Khadamat_UserManagement.Application.Users.Queries.RequestResetCode;
using Khadamat_UserManagement.Contracts.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Khadamat_UserManagement.API.Controllers
{
    [Route("[controller]")]
    public class UsersController : APIController
    {
        private readonly ISender _mediator;
        public UsersController(ISender mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery(request.UserName, request.Password);

            var loginResult = await _mediator.Send(query);

            return loginResult.Match(res =>
            {
                return Ok(new AuthenticationResponse(res.LoginUser.Id, res.LoginUser.UserName, res.LoginUser.Email, res.LoginUser.Roles, res.LoginUser.IsActive, res.Token));
            }, Problem);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new CreateNewUserCommand(request.UserName, request.Password, request.Email, request.Roles, request.IsActive);

            var registerResult = await _mediator.Send(command);

            return registerResult.Match(res =>
            {
                return Ok(new AuthenticationResponse(res.LoginUser.Id, res.LoginUser.UserName, res.LoginUser.Email, res.LoginUser.Roles, res.LoginUser.IsActive, res.Token));
            }, Problem);
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();

            var getAllUsersResult = await _mediator.Send(query);

            return Ok(getAllUsersResult);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {
            var command = new UpdateUserCommand(request.UserName, request.Email, request.Roles, request.IsActive);

            var updateUserCommand = await _mediator.Send(command);

            return updateUserCommand.Match(res =>
            {
                return Ok(res);
            }, Problem);
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var command = new ChangePasswordCommand(request.Password, request.NewPassword);

            var resetPasswordResult = await _mediator.Send(command);

            return resetPasswordResult.Match(_ => Ok(new { Message = "Password Reseted Successfully" }), Problem);
        }

        [AllowAnonymous]
        [HttpGet("requestResetPasswordCode/{email}")]
        public async Task<IActionResult> RequestResetPasswordCode(string email)
        {
            var query = new RequestResetCodeQuery(email);
            var result = await _mediator.Send(query);
            return result.Match(res => Ok(new { CodeExpirationMinutes = res }), Problem);
        }

        [AllowAnonymous]
        [HttpPut("resetPasswordViaCode")]
        public async Task<IActionResult> ResetPasswordViaCode(ResetPasswordViaCodeRequest request)
        {
            var command = new ResetPasswordViaCodeCommand(request.Email, request.Code, request.NewPassword);
            var result = await _mediator.Send(command);
            return result.Match(_ => Ok(new { Message = "Password Reseted Successfully" }), Problem);
        }
    }
}
