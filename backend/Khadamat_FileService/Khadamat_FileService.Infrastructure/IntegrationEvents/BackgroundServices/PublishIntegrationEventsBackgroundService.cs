using Khadamat_FileService.Infrastructure.Common.Persistence;
using Khadamat_SellerPortal.Infrastructure.IntegrationEvents.IntegrationEventsPublisher;
using Khadamat_SharedKernal.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Text.Json;
using Throw;

namespace Khadamat_SellerPortal.Infrastructure.IntegrationEvents.BackgroundServices
{
    public class PublishIntegrationEventsBackgroundService : IHostedService
    {
        private Task? _doWorkTask = null;
        private PeriodicTimer? _timer = null!;
        private readonly IIntegrationEventsPublisher _integrationEventPublisher;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly CancellationTokenSource _cts;

        public PublishIntegrationEventsBackgroundService(IServiceScopeFactory serviceScopeFactory, IIntegrationEventsPublisher integrationEventPublisher)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _integrationEventPublisher = integrationEventPublisher;
            _cts = new CancellationTokenSource();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _doWorkTask = DoWorkAsync();
            return Task.CompletedTask;
        }
        private async Task DoWorkAsync()
        {
            _timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            while (await _timer.WaitForNextTickAsync(_cts.Token))
            {
                try
                {
                    await PublishIntegrationEventsFromDbAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        private async Task PublishIntegrationEventsFromDbAsync()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Khadamat_FileServiceDbContext>();
            var outboxIntegrationEvents = context.OutboxIntegrationEvents.ToList();

            foreach (var e in outboxIntegrationEvents)
            {
                var integrationEvent = JsonSerializer.Deserialize<IIntegrationEvent>(e.EventContent);
                integrationEvent.ThrowIfNull();

                await _integrationEventPublisher.PublishEventAsync(integrationEvent);
                context.Remove(e);
            }
            await context.SaveChangesAsync();
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_doWorkTask is null)
            {
                return;
            }

            _cts.Cancel();
            await _doWorkTask;

            _timer?.Dispose();
            _cts.Dispose();
        }
    }
}
