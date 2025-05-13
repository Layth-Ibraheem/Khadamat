using Khadamat_SellerPortal.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.OnlineSellers.Common
{
    public record SocialMediaLinkDto(SocialMediaLinkType Type, string Link);
}
