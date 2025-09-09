using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.Sellers.Queries.FetchSellerByNationalNo
{
    public class FetchSellerByNationalNoQueryHandler : IRequestHandler<FetchSellerByNationalNoQuery, ErrorOr<Seller>>
    {
        private readonly ISellerRepository _sellerRepository;

        public FetchSellerByNationalNoQueryHandler(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }

        public async Task<ErrorOr<Seller>> Handle(FetchSellerByNationalNoQuery request, CancellationToken cancellationToken)
        {
            var seller = await _sellerRepository.GetSellerByNationalNo(request.NationalNo);
            if (seller == null)
            {
                return Error.NotFound("Seller.NotFound", "There is no seller with such national no");
            }
            return seller;
        }
    }
}
