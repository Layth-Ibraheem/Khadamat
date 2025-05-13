using ErrorOr;
using Khadamat_UserManagement.Application.Common.Authorization;
using Khadamat_UserManagement.Application.Common.Interfaces;
using Khadamat_UserManagement.Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Common.Behaviors
{
    public class RolesCheckBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RolesCheckBehavior(ICurrentUserProvider currentUserProvider, IHttpContextAccessor httpContextAccessor)
        {
            _currentUserProvider = currentUserProvider;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor.HttpContext!.User!.Identity is not { IsAuthenticated: true })
            {
                return await next();
            }

            var currentUser = _currentUserProvider.GetCurrentUser();

            var user = new User(currentUser.UserName, "", currentUser.Email, currentUser.Roles, true, currentUser.Id);

            AuthorizationAttribute? authAttribute = (AuthorizationAttribute?)request.GetType()
                .GetCustomAttributes(typeof(AuthorizationAttribute), false)
                .FirstOrDefault();

            if (authAttribute != null)
            {
                var hasPermissionResult = user.HasAccessTo(authAttribute.Role);
                if (hasPermissionResult.IsError)
                {
                    return (dynamic)hasPermissionResult.FirstError;
                }
            }


            return await next();
        }
    }
}
