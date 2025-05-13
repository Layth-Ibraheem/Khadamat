using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Application.Common.Models;
using Khadamat_UserManagement.Domain.UserAggregate.Events;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Users.Events
{
    public class UserLoggedInEventHandler : INotificationHandler<UserLoggedInDomainEvent>
    {
        private readonly IEmailSender _emailSender;
        private readonly SendingEmailSettings _sendingEmailSettings;
        public UserLoggedInEventHandler(IEmailSender emailSender, IOptions<SendingEmailSettings> sendingEmailSettings)
        {
            _emailSender = emailSender;
            _sendingEmailSettings = sendingEmailSettings.Value;
        }
        public async Task Handle(UserLoggedInDomainEvent notification, CancellationToken cancellationToken)
        {
            await _emailSender.SendEmailAsync(
                _sendingEmailSettings.PersonalEmail,
                notification.Email,
                "Successfull Login",
                $"You have loggedin successfully at [{DateTime.Now:yyyy-MM-dd HH:mm:ss}]");
        }
    }
}
