using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.IntegrationEvents
{
    public record OutboxIntegrationEvent(string EventName, string EventContent);
}
