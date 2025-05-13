using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Domain.UserAggregate
{
    public static partial class Errors
    {
        public static class UserErrors
        {
            public static Error InvalidCredintials = Error.Forbidden("User.InvalidCredintials", "Username or password are not correct");
            public static Error InActiveUser = Error.Forbidden("User.InActiveUser", "This user is not active");
            public static Error UserNameExists = Error.Conflict("User.UserExisist", "This user name already exists");
            public static Error EmailExists = Error.Conflict("User.EmailExists", "This email already exists");
            public static Error NotFound = Error.NotFound("User.NotFound", "This user doesn`t exists");
            public static Error CannotResetPasswordForAnotherUser = Error.Conflict("User.CannotResetPasswordForAnotherUser", "Only the same user can reset his password");
            public static Error InvalidResetToken = Error.Forbidden("User.InvalidResetToken", "Reset code either inncorrect or expired");
        }
    }
}
