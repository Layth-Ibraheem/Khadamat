using Khadamat_SellerPortal.Infrastructure.IntegrationEvents.RabbitMQ;
using Khadamat_SharedKernal.Common;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.IntegrationEvents.IntegrationEventsPublisher
{
    public class IntegrationEventsPublisher : IIntegrationEventsPublisher
    {
        private readonly RabbitMQSettings _rabbitmqSettings;
        private IConnection _connection;
        private IChannel _channel;
        public IntegrationEventsPublisher(IOptions<RabbitMQSettings> rabbitmqSettings)
        {
            _rabbitmqSettings = rabbitmqSettings.Value;

            //IConnectionFactory connectionFactory = new ConnectionFactory
            //{
            //    HostName = _rabbitmqSettings.HostName,
            //    Port = _rabbitmqSettings.Port,
            //    UserName = _rabbitmqSettings.UserName,
            //    Password = _rabbitmqSettings.Password
            //};
            //_connection = connectionFactory.CreateConnectionAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            //_channel = _connection.CreateChannelAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            //_channel.ExchangeDeclareAsync(_rabbitmqSettings.ExchangeName, "fanout", durable: true);
        }
        private async Task EnsureConnectionAsync()
        {
            if (_connection != null && _channel != null) return;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitmqSettings.HostName,
                Port = _rabbitmqSettings.Port,
                UserName = _rabbitmqSettings.UserName,
                Password = _rabbitmqSettings.Password
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(_rabbitmqSettings.ExchangeName, ExchangeType.Fanout, durable: true);
        }
        public async Task PublishEventAsync(IIntegrationEvent integrationEvent)
        {
            await EnsureConnectionAsync();
            string serializedIntegrationEvent = JsonSerializer.Serialize(integrationEvent);

            byte[] body = Encoding.UTF8.GetBytes(serializedIntegrationEvent);

            await _channel.BasicPublishAsync(exchange: _rabbitmqSettings.ExchangeName, routingKey: string.Empty, body: body);
        }
    }
}
