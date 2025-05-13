using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.OnlineSellers
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EducationDegree
    {
        HighSchool = 0,
        Bachelor = 1,
        Master = 2,
        PhD = 3,
        Diploma = 4,
        NotGraduated = 5
    }
}
