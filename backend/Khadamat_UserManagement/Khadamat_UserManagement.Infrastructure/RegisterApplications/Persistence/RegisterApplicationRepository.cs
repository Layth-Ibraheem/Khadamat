using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Domain.RegisterApplicationAggregate;
using Khadamat_UserManagement.Domain.UserAggregate;
using Khadamat_UserManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Infrastructure.RegisterApplications.Persistence
{
    public class RegisterApplicationRepository : IRegisterApplicationRepository
    {
        private readonly Khadamat_UserDbContext _context;
        public RegisterApplicationRepository(Khadamat_UserDbContext context)
        {
            _context = context;
        }
        public async Task AddRegisterApplicationAsync(RegisterApplication Application)
        {
            await _context.RegisterApplications.AddAsync(Application);
        }

        public async Task ApproveRegisterApplicationAsync(int Id)
        {
            await _context.RegisterApplications
                .Where(ra => ra.Id == Id)
                .ExecuteUpdateAsync(setters =>
                    setters.SetProperty(p => p.Status, ApplicationStatus.Approved));
        }

        public async Task<IReadOnlyList<RegisterApplication>> GetAllApplicationsAsyc(ApplicationStatus? Status = null, int? UserId = null)
        {
            var query = _context.RegisterApplications.AsQueryable();

            if (Status is not null)
            {
                query = query.Where(ra => ra.Status == Status);
            }

            if (UserId.HasValue)
            {
                query = query.Where(ra => ra.HandledByUserId == UserId);
            }

            return await query.ToListAsync();
        }

        public async Task<RegisterApplication?> GetApplicationByIdAsync(int Id)
        {
            return await _getApplicationBy(ra => ra.Id == Id);
        }

        public async Task<RegisterApplication?> GetApplicationByUsernameAsync(string Username)
        {
            return await _getApplicationBy(ra => ra.Username == Username);
        }
        private async Task<RegisterApplication?> _getApplicationBy(Expression<Func<RegisterApplication, bool>> condition)
        {
            return await _context.RegisterApplications.Where(condition).FirstOrDefaultAsync();
        }

        public async Task RejectRegisterApplicationAsync(int Id)
        {
            await _context.RegisterApplications
                .Where(ra => ra.Id == Id)
                .ExecuteUpdateAsync(setters =>
                    setters.SetProperty(p => p.Status, ApplicationStatus.Rejected));
        }

        public async Task UpdateRegisterApplicationAsync(RegisterApplication Application)
        {
            _context.RegisterApplications.Update(Application);
            await Task.CompletedTask;
        }

        public async Task<RegisterApplication?> GetApplicationByEmailAsync(string Email)
        {
            return await _getApplicationBy(ra => ra.Email == Email);
        }

        public async Task<bool> IsApplicationExists(string Username)
        {
            return await _context.RegisterApplications.Where(ra => ra.Username == Username && ra.Status == ApplicationStatus.Draft).AnyAsync();
        }
    }
}
