using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Khadamat_SellerPortal.Contracts.Educations;
using Khadamat_SellerPortal.Contracts.PortfolioURLs;
using Khadamat_SellerPortal.Contracts.Sellers;
using Khadamat_SellerPortal.Contracts.SocialMediaLinks;
using Khadamat_SellerPortal.Contracts.WorkExperiences;

namespace Khadamat_SellerPortal.Contracts.OnlineSellers
{
    public record OnlineSellerResponse(SellerResponse BaseSellerInfo);
}
