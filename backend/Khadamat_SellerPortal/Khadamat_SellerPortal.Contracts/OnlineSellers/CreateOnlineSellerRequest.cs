﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Khadamat_SellerPortal.Contracts.Educations;
using Khadamat_SellerPortal.Contracts.PortfolioURLs;
using Khadamat_SellerPortal.Contracts.SocialMediaLinks;
using Khadamat_SellerPortal.Contracts.WorkExperiences;

namespace Khadamat_SellerPortal.Contracts.OnlineSellers
{
    public record CreateOnlineSellerRequest(
        string FirstName,
        string SecondName,
        string LastName,
        string Email,
        string NationalNo,
        DateTime DateOfBirth,
        string Country,
        string City,
        string Region,
        List<AddPortfolioUrlRequest> PortfolioUrls,
        List<AddSocialMediaLinkRequest> SocialMediaLinks,
        List<AddWorkExperienceRequest> WorkExperiences,
        List<AddEducationRequest> Educations);
}
