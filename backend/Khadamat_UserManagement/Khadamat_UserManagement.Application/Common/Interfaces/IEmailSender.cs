using Khadamat_UserManagement.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string from, string to, string subject, string body, bool isHtml = false);
        Task SendEmailAsync(string from, List<string> to, string subject, string body, bool isHtml = false);
        Task SendPasswordResetCodeAsync(User user, string resetCode);
    }
}
