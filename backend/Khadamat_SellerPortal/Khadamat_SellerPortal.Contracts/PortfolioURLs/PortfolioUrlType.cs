using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.PortfolioURLs
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PortfolioUrlType
    {
        Git = 0,
        PersonalWebsite = 1,
        KhadamatPortfolio = 2
    }
}
