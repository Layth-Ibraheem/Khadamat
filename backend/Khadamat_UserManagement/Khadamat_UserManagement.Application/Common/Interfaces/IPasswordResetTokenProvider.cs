using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Common.Interfaces
{
    public interface IPasswordResetCodeProvider
    {
        (string, double) GenerateResetCode(string email);
        bool ValidateResetCode(string email, string code);
    }
}
