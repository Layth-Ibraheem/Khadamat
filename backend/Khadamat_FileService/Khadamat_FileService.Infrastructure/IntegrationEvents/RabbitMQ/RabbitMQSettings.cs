using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.IntegrationEvents.RabbitMQ
{
    public class RabbitMQSettings
    {
        public const string Section = "RabbitMQ";

        public string HostName { get; set; } = null!;
        public string QueueName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ExchangeName { get; set; } = null!;
        public int RetryCount { get; set; }
        public int Port { get; set; }

    }
}
