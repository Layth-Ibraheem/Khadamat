using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.OnlineSellers.Queries.FetchOnlineSeller
{
    public class FetchOnlineSellerQueryHandler : IRequestHandler<FetchOnlineSellerQuery, ErrorOr<OnlineSeller?>>
    {
        private readonly IOnlineSellerRepository _onlineSellerRepository;

        public FetchOnlineSellerQueryHandler(IOnlineSellerRepository onlineSellerRepository)
        {
            _onlineSellerRepository = onlineSellerRepository;
        }

        public async Task<ErrorOr<OnlineSeller?>> Handle(FetchOnlineSellerQuery request, CancellationToken cancellationToken)
        {
            var onlineSeller = await _onlineSellerRepository.GetOnlineSellerById(request.Id);
            return onlineSeller;
        }
    }
}
