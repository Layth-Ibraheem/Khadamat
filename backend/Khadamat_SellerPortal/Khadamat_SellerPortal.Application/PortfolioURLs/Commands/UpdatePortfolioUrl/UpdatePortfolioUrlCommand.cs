using ErrorOr;
using Khadamat_SellerPortal.Domain.Common.Entities;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.PortfolioURLs.Commands.UpdatePortfolioUrl
{
    public record UpdatePortfolioUrlCommand(int SellerId, PortfolioUrlType Type, string Url) : IRequest<ErrorOr<OnlineSeller>>;
}
