using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Application.Common.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Khadamat_UserManagement.API.CurrentUserProviderInterface
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public CurrentUser GetCurrentUser()
        {
            try
            {

                var claims = _httpContextAccessor.HttpContext!.User.Claims;

                int id = Convert.ToInt32(claims.First(c => c.Type == "id").Value);

                string userName = claims.First(c => c.Type == "userName").Value;

                var roles = Convert.ToInt32(claims.First(c => c.Type == "userRoles").Value);

                
                var email = claims.First(c => c.Type == ClaimTypes.Email).Value;


                return new CurrentUser(id, userName, email, roles);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}
