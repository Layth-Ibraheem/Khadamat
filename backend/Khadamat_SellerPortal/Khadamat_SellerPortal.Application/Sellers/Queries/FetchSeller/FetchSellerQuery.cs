using ErrorOr;
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
    public record FetchSellerQuery(int id) : IRequest<ErrorOr<Seller?>>;
}
