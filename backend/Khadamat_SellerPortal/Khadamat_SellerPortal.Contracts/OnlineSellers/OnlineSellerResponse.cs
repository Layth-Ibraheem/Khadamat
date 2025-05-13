using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.OnlineSellers
{
    public record OnlineSellerResponse(
        int id,
        string firstName,
        string secondName,
        string lastName,
        string email,
        string nationalNo,
        DateTime dateOfBirth,
        string country,
        string city,
        string region,
        List<PortfolioUrlResponse> portfolioUrls,
        List<SocialMediaLinkResponse> socialMediaLinks,
        List<WorkExperienceResponse> workExperiences,
        List<EducationResponse> educations);
}
