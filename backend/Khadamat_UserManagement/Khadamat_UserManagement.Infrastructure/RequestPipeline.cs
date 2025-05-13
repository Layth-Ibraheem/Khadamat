using Khadamat_UserManagement.Infrastructure.Common.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Khadamat_UserManagement.Infrastructure
{
    public static class RequestPipeline
    {
        public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<EventualConsistencyMiddleware>();
            return app;
        }
    }
}
