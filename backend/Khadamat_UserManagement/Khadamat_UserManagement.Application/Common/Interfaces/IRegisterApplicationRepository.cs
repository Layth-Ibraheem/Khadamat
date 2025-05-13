using Khadamat_UserManagement.Domain.RegisterApplicationAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Common.Interfaces
{
    public interface IRegisterApplicationRepository
    {
        Task AddRegisterApplicationAsync(RegisterApplication Application);
        Task UpdateRegisterApplicationAsync(RegisterApplication Application);
        Task ApproveRegisterApplicationAsync(int Id);
        Task RejectRegisterApplicationAsync(int Id);
        Task<RegisterApplication?> GetApplicationByIdAsync(int Id);
        Task<RegisterApplication?> GetApplicationByUsernameAsync(string Username);
        Task<RegisterApplication?> GetApplicationByEmailAsync(string Email);
        Task<bool> IsApplicationExists(string Username);
        Task<IReadOnlyList<RegisterApplication>> GetAllApplicationsAsyc(ApplicationStatus? Status = null, int? UserId = null);
    }
}
