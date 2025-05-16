using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.OnlineSellers
{
    public record OnlineSellerResponse(
        int Id,
        string FirstName,
        string SecondName,
        string LastName,
        string Email,
        string NationalNo,
        DateTime DateOfBirth,
        string Country,
        string City,
        string Region,
        List<PortfolioUrlResponse> PortfolioUrls,
        List<SocialMediaLinkResponse> SocialMediaLinks,
        List<WorkExperienceResponse> WorkExperiences,
        List<EducationResponse> Educations);
}
