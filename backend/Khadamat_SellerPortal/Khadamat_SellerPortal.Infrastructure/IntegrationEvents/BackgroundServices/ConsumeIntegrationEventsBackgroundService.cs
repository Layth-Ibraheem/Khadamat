using Khadamat_SellerPortal.Infrastructure.IntegrationEvents.RabbitMQ;
using Khadamat_SharedKernal.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Throw;

namespace Khadamat_SellerPortal.Infrastructure.IntegrationEvents.BackgroundServices
{
    public class ConsumeIntegrationEventsBackgroundService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly CancellationTokenSource _cts;
        private IChannel _channel;
        private IConnection _connection;
        private readonly RabbitMQSettings _rabbitmqSettings;

        public ConsumeIntegrationEventsBackgroundService(IOptions<RabbitMQSettings> rabbitmqSettings, IServiceScopeFactory scopeFactory)
        {
            _rabbitmqSettings = rabbitmqSettings.Value;
            _scopeFactory = scopeFactory;
            _cts = new CancellationTokenSource();
        }

        private async Task PublishIntegrationEvent(object sender, BasicDeliverEventArgs eventArgs)
        {
            if (_cts.IsCancellationRequested)
            {
                return;
            }

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var integrationEvent = JsonSerializer.Deserialize<IIntegrationEvent>(message);
                integrationEvent.ThrowIfNull();

                var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
                await publisher.Publish(integrationEvent);

                await _channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                IConnectionFactory connectionFactory = new ConnectionFactory
                {
                    HostName = _rabbitmqSettings.HostName,
                    Port = _rabbitmqSettings.Port,
                    UserName = _rabbitmqSettings.UserName,
                    Password = _rabbitmqSettings.Password
                };

                _connection = await connectionFactory.CreateConnectionAsync();
                _channel = await _connection.CreateChannelAsync();

                await _channel.ExchangeDeclareAsync(_rabbitmqSettings.ExchangeName, ExchangeType.Fanout, durable: true);

                await _channel.QueueDeclareAsync(
                    queue: _rabbitmqSettings.QueueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false);

                await _channel.QueueBindAsync(
                    queue: _rabbitmqSettings.QueueName,
                    exchange: _rabbitmqSettings.ExchangeName,
                    routingKey: string.Empty);

                var consumer = new AsyncEventingBasicConsumer(_channel);
                consumer.ReceivedAsync += PublishIntegrationEvent;

                await _channel.BasicConsumeAsync(_rabbitmqSettings.QueueName, autoAck: false, consumer);

                Debug.WriteLine("Consumer successfully started.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Error in Consumer StartAsync: {ex}");
                throw; // important so the host knows the service failed
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cts.Cancel();
            _cts.Dispose();
            return Task.CompletedTask;
        }
    }
}
