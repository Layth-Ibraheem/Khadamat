using Khadamat_UserManagement.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Common.Authorization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AuthorizationAttribute : Attribute
    {
        public UserRole Role { get; set; }
        public AuthorizationAttribute(UserRole role)
        {
            Role = role;
        }
    }
}
