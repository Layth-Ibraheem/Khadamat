using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Domain.UserAggregate;
using Khadamat_UserManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Infrastructure.Users.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly Khadamat_UserDbContext _context;
        public UserRepository(Khadamat_UserDbContext context)
        {
            _context = context;
        }
        public async Task AddNewUserAsync(User User)
        {
            await _context.Users.AddAsync(User);
        }

        public async Task DeleteUserAsync(User User)
        {
            await Task.CompletedTask;
            _context.Users.Remove(User);
        }

        public async Task<User?> GetUserByEmailAsync(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
        }

        public async Task<User?> GetUserByUserNameAsync(string UserName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName);
        }

        public async Task<List<User>> GetUseresListAsync()
        {
            return await _context.Users.OrderByDescending(u => u.Id).OrderByDescending(u => u.IsActive).AsNoTracking().ToListAsync();
        }

        public async Task<List<User>> GetUseresListAsync(UserRole role)
        {
            return await _context.Users.Where(u => u.Roles == (int)role).AsNoTracking().ToListAsync();
        }

        public async Task<bool> IsUserExistsByEmailAsync(string Email)
        {
            return await _context.Users.Where(u => u.Email == Email).AnyAsync();
        }

        public async Task<bool> IsUserExistsByUserNameAsync(string UserName)
        {
            return await _context.Users.Where(u => u.UserName == UserName).AnyAsync();
        }

        public async Task UpdateUserAsync(User User)
        {
            await Task.CompletedTask;
            _context.Update(User);
        }
    }
}
