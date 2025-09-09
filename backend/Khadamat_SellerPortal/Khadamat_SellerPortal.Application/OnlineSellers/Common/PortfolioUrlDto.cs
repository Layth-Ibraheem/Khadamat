using Khadamat_SellerPortal.Domain.Common.Entities.PortfolioUrlEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.OnlineSellers.Common
{
    public record PortfolioUrlDto(PortfolioUrlType Type, string Url);
}
