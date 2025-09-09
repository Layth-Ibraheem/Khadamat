using Khadamat_SharedKernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.IntegrationEvents.IntegrationEventsPublisher
{
    public interface IIntegrationEventsPublisher
    {
        public Task PublishEventAsync(IIntegrationEvent integrationEvent);
    }
}
