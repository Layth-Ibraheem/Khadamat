using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.OnlineSellers
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SocialMediaLinkType
    {
        Facebook = 0,
        Instagram = 1,
        X = 2,
        Youtube = 3,
        Linkedin = 4
    }
}
