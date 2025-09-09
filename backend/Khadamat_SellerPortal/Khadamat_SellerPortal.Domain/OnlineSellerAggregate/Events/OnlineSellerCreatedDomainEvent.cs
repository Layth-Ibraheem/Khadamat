using Khadamat_SellerPortal.Domain.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.OnlineSellerAggregate.Events
{
    public record OnlineSellerCreatedDomainEvent(string NationalNumber) : IDomainEvent;
}
