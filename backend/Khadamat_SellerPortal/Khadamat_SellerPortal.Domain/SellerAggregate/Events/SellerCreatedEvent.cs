using Khadamat_SellerPortal.Domain.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.SellerAggregate.Events
{
    public record SellerCreatedEvent : IDomainEvent
    {
        private readonly Func<int> _sellerIdProvider;
        public int SellerId => _sellerIdProvider();

        public SellerCreatedEvent(Func<int> sellerIdProvider)
        {
            _sellerIdProvider = sellerIdProvider;
        }
    }
}
