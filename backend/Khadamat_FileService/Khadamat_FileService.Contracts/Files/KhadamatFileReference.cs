using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Khadamat_FileService.Contracts.Files
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum KhadamatFileReference
    {
        SellerEducationCertificate = 0,
        SellerWorkExperienceCertificate = 1,
        SellerProfileImage = 2,
        ChattingFile = 3,
    }
}
