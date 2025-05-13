using Khadamat_UserManagement.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task AddNewUserAsync(User User);
        Task UpdateUserAsync(User User);
        Task DeleteUserAsync(User User);
        Task<User?> GetUserByUserNameAsync(string UserName);
        Task<User?> GetUserByEmailAsync(string Email);
        Task<bool> IsUserExistsByUserNameAsync(string UserName);
        Task<bool> IsUserExistsByEmailAsync(string Email);
        Task<List<User>> GetUseresListAsync();
        Task<List<User>> GetUseresListAsync(UserRole role);
    }
}
