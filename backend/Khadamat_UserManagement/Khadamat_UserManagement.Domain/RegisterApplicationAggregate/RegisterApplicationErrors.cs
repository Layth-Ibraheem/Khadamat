using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Domain.RegisterApplicationAggregate
{
    public static partial class Errors
    {
        public static class RegisterApplicationErrors
        {
            public static Error ApplicationHandled = Error.Forbidden("User.ApplicationHandled", "This application is already handled");
            public static Error AlreadyExists = Error.Forbidden("User.AlreadyExists", "There is already a pending application");
            public static Error NotFound = Error.Forbidden("User.NotFound", "There is no such application");
        }
    }
}
