using Khadamat_UserManagement.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Common.Models
{
    public record CurrentUser(int Id, string UserName, string Email, int Roles)
    {
        /// <summary>
        /// return an empty user with the email of the admin
        /// </summary>
        /// <returns></returns>
        public static CurrentUser GetDefault()
        {
            return new CurrentUser(0, "", "laythibraheem97@gmail.com", 0); // replace later with the company mail
        }
        public User GetActualUser()
        {
            return new User(UserName, "", Email, Roles, true, Id);
        }
    }
}
