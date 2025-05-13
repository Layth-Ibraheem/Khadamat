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
    public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredDomainEvent>
    {
        private readonly IEmailSender _emailSender;
        private readonly SendingEmailSettings _sendingEmailSettings;
        public UserRegisteredEventHandler(IEmailSender emailSender, IOptions<SendingEmailSettings> sendingEmailSettings)
        {
            _emailSender = emailSender;
            _sendingEmailSettings = sendingEmailSettings.Value;
        }
        public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            await _emailSender.SendEmailAsync(
                _sendingEmailSettings.PersonalEmail,
                notification.Email,
                "Welcoming Message",
                $"Hello Mr/Ms. {notification.UserName}, Enjoy using our services and don`t hesitate calling us at any time...❤");
        }
    }
}
