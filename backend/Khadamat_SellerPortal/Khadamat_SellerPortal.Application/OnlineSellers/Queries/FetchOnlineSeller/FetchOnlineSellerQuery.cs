using ErrorOr;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.OnlineSellers.Queries.FetchOnlineSeller
{
    public record FetchOnlineSellerQuery(int id) : IRequest<ErrorOr<OnlineSeller?>>;
}
