using Khadamat_SharedKernal.Khadamat_SellerPortal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Application.KhadamatFiles.IntegrationEvents
{
    public class SellerCreatedIntegrationEventHandler : INotificationHandler<SellerCreatedIntegrationEvent>
    {
        public async Task Handle(SellerCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            // do something
        }
    }
}
