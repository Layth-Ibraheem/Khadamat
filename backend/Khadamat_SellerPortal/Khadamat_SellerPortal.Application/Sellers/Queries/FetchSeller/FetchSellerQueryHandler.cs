using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.Sellers.Queries.FetchSeller
{
    public class FetchSellerQueryHandler : IRequestHandler<FetchSellerQuery, ErrorOr<Seller?>>
    {
        private readonly ISellerRepository _sellerRepository;
        public FetchSellerQueryHandler(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }
        public async Task<ErrorOr<Seller?>> Handle(FetchSellerQuery request, CancellationToken cancellationToken)
        {
            var OS = await _sellerRepository.GetSellerById(request.id);
            return OS;
        }
    }
}
