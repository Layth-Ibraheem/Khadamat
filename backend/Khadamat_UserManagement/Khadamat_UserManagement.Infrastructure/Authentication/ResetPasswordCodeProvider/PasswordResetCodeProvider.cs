using Khadamat_UserManagement.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Infrastructure.Authentication.ResetPasswordCodeProvider
{
    public class PasswordResetCodeProvider : IPasswordResetCodeProvider
    {
        private readonly TimeSpan _codeLifeSpan = TimeSpan.FromMinutes(2);
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private readonly double _expirationInMinutes;
        public PasswordResetCodeProvider(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            _configuration = configuration;
            _expirationInMinutes = double.Parse(configuration["CodeExpirationMinutes"]!);
            _codeLifeSpan = TimeSpan.FromMinutes(_expirationInMinutes);
        }
        public (string, double) GenerateResetCode(string email)
        {
            if (_memoryCache.TryGetValue($"reset_code_{email}", out string? _))
            {
                _memoryCache.Remove($"reset_code_{email}");
            }
            string code = Guid.NewGuid().ToString("N")[..9].ToUpper();
            _memoryCache.Set($"reset_code_{email}", code, _codeLifeSpan);

            return (code, _expirationInMinutes);
        }

        public bool ValidateResetCode(string email, string code)
        {
            if (_memoryCache.TryGetValue($"reset_code_{email}", out string? cachedCode))
            {
                if (cachedCode == code)
                {
                    _memoryCache.Remove($"reset_code_{email}");
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
