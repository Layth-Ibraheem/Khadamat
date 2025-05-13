using ErrorOr;
using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Application.Common.Logging;
using Khadamat_UserManagement.Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Users.Queries.RequestResetCode;

public class RequestResetCodeQueryHandler : IRequestHandler<RequestResetCodeQuery, ErrorOr<double>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordResetCodeProvider _passwordResetCodeProvider;
    private readonly IEmailSender _emailSender;
    private readonly ILogsCollector _logsCollector;

    public RequestResetCodeQueryHandler(IUserRepository userRepository, IPasswordResetCodeProvider passwordResetCodeProvider, IEmailSender emailSender, ILogsCollector logsCollector)
    {
        _userRepository = userRepository;
        _passwordResetCodeProvider = passwordResetCodeProvider;
        _emailSender = emailSender;
        _logsCollector = logsCollector;
    }
    public async Task<ErrorOr<double>> Handle(RequestResetCodeQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user is null)
        {
            return Errors.UserErrors.NotFound;
        }
        var result = _passwordResetCodeProvider.GenerateResetCode(user.Email);
        await _emailSender.SendPasswordResetCodeAsync(user, result.Item1);
        _logsCollector.AddLog(new Log(Id: 0, UserId: 0, Message: $"An email with the reset code {result.Item1} has been sent to the user with email {user.Email}", Level: "Information", TimeStamp: DateTime.Now));
        return result.Item2;
    }
}
