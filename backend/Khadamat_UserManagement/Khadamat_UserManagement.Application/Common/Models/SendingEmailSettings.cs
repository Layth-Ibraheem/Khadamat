using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Common.Models
{
    public class SendingEmailSettings
    {
        public const string Section = "SendingEmailSettings";

        public string PersonalEmail { get; set; } = null!;
        public string CompanyEmail { get; set; } = null!;
    }
}
