using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Application.Users.Commands.CreateNewUser;
using Khadamat_UserManagement.Domain.Common.Interfaces;
using Khadamat_UserManagement.Domain.RegisterApplicationAggregate.Events;
using Khadamat_UserManagement.Domain.UserAggregate;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.RegisterApplications.Events
{
    public class ApplicationApprovedEventHandler : INotificationHandler<ApplicationApprovedDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRegisterApplicationRepository _registerRepository;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        public ApplicationApprovedEventHandler(IUnitOfWork unitOfWork, IEmailSender emailSender, IRegisterApplicationRepository registerRepository, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _registerRepository = registerRepository;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task Handle(ApplicationApprovedDomainEvent notification, CancellationToken cancellationToken)
        {
            var application = await _registerRepository.GetApplicationByUsernameAsync(notification.username);

            if (application == null)
            {
                throw new Exception($"Application for user {notification.username} wasn`t found");
            }
            var user = new User(application.Username, _passwordHasher.HashPassword(application.Password), application.Email, application.Roles, true);

            var approvedEmailBody = $@"
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
                            color: #2e7d32;
                            font-size: 24px;
                            font-weight: bold;
                            margin-bottom: 20px;
                        }}
                        .button {{
                            display: inline-block;
                            padding: 12px 24px;
                            background-color: #2e7d32;
                            color: white !important;
                            text-decoration: none;
                            border-radius: 4px;
                            font-weight: bold;
                            margin: 15px 0;
                        }}
                        .footer {{
                            margin-top: 30px;
                            font-size: 12px;
                            color: #777777;
                        }}
                    </style>
                </head>
                <body>
                    <div class='header'>Application Approved</div>
    
                    <p>Dear {notification.username},</p>
    
                    <p>We are pleased to inform you that your registration application has been approved.</p>
    
                    <p>You can now access your account using the credentials you provided during registration.</p>
    
                    <p>
                        <a href='http://localhost:4200/login' class='button'>Login to Your Account</a>
                    </p>
    
                    <p>If you have any questions or need assistance, contact Mr/Ms.{notification.connectToAdminUsername} at {notification.connectToAdminEmail}</p>
    
                    <div class='footer'>
                        <p>Best regards,<br>The Khadamat Users Managemenet Team</p>
                        <p>© {DateTime.Now.Year} Khadamat Users Managemenet. All rights reserved.</p>
                    </div>
                </body>
                </html>
                ";

            await _userRepository.AddNewUserAsync(user);
            await _unitOfWork.CommitChangesAsync();
            await _emailSender.SendEmailAsync(notification.connectToAdminEmail, notification.email, "Application Approved", approvedEmailBody, true);
        }
    }
}
