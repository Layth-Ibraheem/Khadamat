using ErrorOr;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.OnlineSellers.Commands
{
    public record RegisterNewOnlineSellerCommand(
        string FirstName,
        string SecondName,
        string LastName,
        string Email,
        string? NationalNo,
        DateTime DateOfBirth,
        string Country,
        string City,
        string Region,
        List<WorkExperienceDto> WorkExperiences,
        List<EducationDto> Educations,
        List<PortfolioUrlDto> PortfolioUrls,
        List<SocialMediaLinkDto> SocialMediaLinks) : IRequest<ErrorOr<OnlineSeller>>;
}
