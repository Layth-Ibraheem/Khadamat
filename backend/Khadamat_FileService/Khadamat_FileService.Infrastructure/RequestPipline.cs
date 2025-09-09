using Khadamat_FileService.Infrastructure.Common.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Infrastructure
{
    public static class RequestPipeline
    {
        public static IApplicationBuilder UseInfrastructureMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<EventualConsistencyMiddleware>();
            return app;
        }
    }
}
