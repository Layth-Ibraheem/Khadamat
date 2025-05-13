using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Application.Common.Models;
using Khadamat_UserManagement.Domain.Common.Interfaces;
using Khadamat_UserManagement.Domain.RegisterApplicationAggregate.Events;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.RegisterApplications.Events
{
    public class ApplicationRejectedEventHandler : INotificationHandler<ApplicationRejectedDomainEvent>
    {
        private readonly IRegisterApplicationRepository _registerRepository;
        private readonly IEmailSender _emailSender;
        private readonly SendingEmailSettings _sendingEmailSettings;
        public ApplicationRejectedEventHandler(IEmailSender emailSender, IRegisterApplicationRepository registerRepository, IOptions<SendingEmailSettings> sendingEmailSettings)
        {
            _emailSender = emailSender;
            _registerRepository = registerRepository;
            _sendingEmailSettings = sendingEmailSettings.Value;
        }

        public async Task Handle(ApplicationRejectedDomainEvent notification, CancellationToken cancellationToken)
        {
            var application = _registerRepository.GetApplicationByUsernameAsync(notification.username);
            if (application == null)
            {
                throw new Exception($"Application for user {notification.username} wasn`t found");
            }

            var rejectionEmailBody = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: 'Segoe UI', Arial, sans-serif;
                            line-height: 1.6;
                            color: #333333;
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                        }}
                        .header {{
                            color: #c62828;
                            font-size: 24px;
                            font-weight: bold;
                            margin-bottom: 20px;
                        }}
                        .reason-box {{
                            background-color: #ffebee;
                            border-left: 4px solid #c62828;
                            padding: 12px;
                            margin: 15px 0;
                        }}
                        .action-button {{
                            display: inline-block;
                            padding: 12px 24px;
                            background-color: #1565c0;
                            color: white !important;
                            text-decoration: none;
                            border-radius: 4px;
                            font-weight: bold;
                            margin-top: 15px;
                        }}
                        .footer {{
                            margin-top: 30px;
                            font-size: 12px;
                            color: #777777;
                        }}
                    </style>
                </head>
                <body>
                    <div class='header'>Application Status Update</div>
    
                    <p>Dear {notification.username},</p>
    
                    <p>Thank you for submitting your registration application. After careful review, we regret to inform you that your application has not been approved.</p>
    
                    <div class='reason-box'>
                        <strong>Reason for rejection:</strong>
                        <p>{notification.rejectionReason}</p>
                    </div>
    
                    <p>If you believe this decision was made in error, or would like to clarify any information, you may:</p>
    
                    <ul>
                        <li>Submit a new application with additional supporting documents</li>
                        <li>Contact our support team for further clarification</li>
                    </ul>
    
                    <p>
                        <a href='https://localhost:4200/register' class='action-button' style='background-color: #2e7d32; margin-left: 10px;'>Reapply</a>
                    </p>
    
                    <div class='footer'>
                        <p>Best regards,<br>The Khadamat Users Management Team</p>
                        <p>© {DateTime.Now.Year} Khadamat Users Management. All rights reserved.</p>
                    </div>
                </body>
                </html>
                ";

            await _emailSender.SendEmailAsync(_sendingEmailSettings.PersonalEmail, notification.email, "Application Rejected", rejectionEmailBody, true);

        }
    }
}
