using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.SocialMediaLinks
{
    public record UpdateSocialMediaLinkRequest(SocialMediaLinkType Type, string Link);
}
