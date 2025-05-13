using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Infrastructure.Common.Email
{
    public class EmailSettings
    {
        public const string Section = "EmailSettings";

        public string SenderName { get; set; } = null!;
        public string SmtpServer { get; set; } = null!;
        public string SmtpUsername { get; set; } = null!;
        public string SmtpPassword { get; set; } = null!;
        public int SmtpPort { get; set; }
        public bool UseSsl { get; set; }
    }
}
