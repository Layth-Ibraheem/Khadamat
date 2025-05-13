using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Domain.RegisterApplicationAggregate.Events;
using MediatR;
using Khadamat_UserManagement.Domain.UserAggregate;
using Microsoft.Extensions.Configuration;
using Khadamat_UserManagement.Application.Common.Models;
using Microsoft.Extensions.Options;
namespace Khadamat_UserManagement.Application.RegisterApplications.Events
{
    public class ApplicationCreatedEventHandler : INotificationHandler<ApplicationCreatedDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly SendingEmailSettings _sendingEmailSettings;
        public ApplicationCreatedEventHandler(IUserRepository userRepository, IEmailSender emailSender, IOptions<SendingEmailSettings> sendingEmailSettings)
        {
            _userRepository = userRepository;
            _emailSender = emailSender;
            _sendingEmailSettings = sendingEmailSettings.Value;
        }
        public async Task Handle(ApplicationCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var adminUsers = await _userRepository.GetUseresListAsync(UserRole.Admin);
            var emails = adminUsers.Select(x => x.Email).ToList();

            var body = $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
                        .action-button {{ 
                            display: inline-block; 
                            padding: 8px 16px; 
                            margin: 0 5px; 
                            color: white; 
                            text-decoration: none; 
                            border-radius: 4px; 
                            font-weight: bold;
                        }}
                        .approve {{ background-color: #28a745; }}
                        .reject {{ background-color: #dc3545; }}
                    </style>
                </head>
                <body>
                    <p>Dear Administrator,</p>
    
                    <p>A new registration application has been submitted with the following details:</p>
    
                    <ul>
                        <li><strong>User Name:</strong> {notification.username}</li>
                        <li><strong>Email:</strong> {notification.email}</li>
                        <li><strong>Submission Date:</strong> {DateTime.Now:MMMM dd, yyyy}</li>
                    </ul>
    
                    <p>Please review and take appropriate action:</p>
    
                    <p>
                        <a href='http://localhost:4200/home/applications' </a>
                        <a href='http://localhost:4200/home/applications' </a>
                    </p>
    
                    <p>Alternatively, you can review all pending applications at:<br>
                    <a href='http://localhost:4200/home/applications'>Application Dashboard</a></p>
    
                    <p>Best regards,<br>
                    The Registration Team</p>
                </body>
                </html>
                ";

            await _emailSender.SendEmailAsync(_sendingEmailSettings.PersonalEmail, emails, "New Register Application", body, true);
        }
    }
}
