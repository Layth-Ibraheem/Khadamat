using Khadamat_FileService.Application.Common.Interfaces;
using Khadamat_FileService.Domain.Common.Events;
using Khadamat_FileService.Infrastructure.Common.Persistence;
using Khadamat_SellerPortal.Infrastructure.IntegrationEvents.IntegrationEventsPublisher;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Infrastructure.Common.Middleware
{
    public class EventualConsistencyMiddleware
    {
        private readonly RequestDelegate _next;

        public EventualConsistencyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IPublisher publisher, Khadamat_FileServiceDbContext dbContext, IIntegrationEventsPublisher integrationEventsPublisher)
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            context.Response.OnCompleted(async () =>
            {
                if (context.Items.TryGetValue("DomainEventsKey", out var value) && value is Queue<IDomainEvent> domainEvents)
                {

                    try
                    {
                        while (domainEvents.TryDequeue(out var nextEvent))
                        {
                            await publisher.Publish(nextEvent);
                        }

                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Source + ": ", ex.Message);
                        await transaction.RollbackAsync();
                    }
                    finally
                    {
                        await transaction.DisposeAsync();
                    }

                }
                else
                {
                    await transaction.CommitAsync();
                    await transaction.DisposeAsync();
                }
            });
            await _next(context);
        }

    }
}
