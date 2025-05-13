using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Domain.RegisterApplicationAggregate
{
    public class ApplicationStatus : SmartEnum<ApplicationStatus>
    {
        public static readonly ApplicationStatus Rejected = new ApplicationStatus("Rejected", 0);
        public static readonly ApplicationStatus Approved = new ApplicationStatus("Approved", 1);
        public static readonly ApplicationStatus Draft = new ApplicationStatus("Draft", 2);
        public ApplicationStatus(string name, int value) : base(name, value)
        {
        }
    }
}
