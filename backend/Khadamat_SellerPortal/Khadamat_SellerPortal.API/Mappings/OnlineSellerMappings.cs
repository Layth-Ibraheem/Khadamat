using Mapster;
using Khadamat_SellerPortal.Contracts.OnlineSellers;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;
using System.ComponentModel.DataAnnotations.Schema;
using Khadamat_SellerPortal.Domain.Common.Entities;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using Khadamat_SellerPortal.Application.OnlineSellers.Commands.RegisterOnlineSeller;
using Khadamat_SellerPortal.Application.Sellers.Commands.UpdateOnlineSellerPersonalInfo;
using Khadamat_SellerPortal.Contracts.PortfolioURLs;
using Khadamat_SellerPortal.Contracts.SocialMediaLinks;
using Khadamat_SellerPortal.Contracts.Certificates;
using Khadamat_SellerPortal.Contracts.Educations;
using Khadamat_SellerPortal.Contracts.WorkExperiences;
using Khadamat_SellerPortal.Contracts.Sellers;
using System.Linq;


namespace Khadamat_SellerPortal.API.Mappings
{
    public class OnlineSellerMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            ConfigMappingFromRequest(config);
            // Main OnlineSeller to OnlineSellerResponse mapping
            config.NewConfig<OnlineSeller, OnlineSellerResponse>()
                .Map(dest => dest.BaseSellerInfo, src => MapToSellerResponse(src));
        }
        private SellerResponse MapToSellerResponse(OnlineSeller src)
        {
            //var portfolioUrlResponses = src.PortfolioUrls.Select(p => new PortfolioUrlResponse(p.Id, p.Url, p.Type.Name)).ToList();
            //var socialMediaLinkResponses = src.SocialMediaLinks.Select(l => new SocialMediaLinkResponse(l.Id, l.Link, l.Type.Name)).ToList();
            //var workExperienceResponses = src.WorkExperiences.Select(w => new WorkExperienceResponse(
            //    w.Id, w.CompanyName, w.Position, w.Field, w.Range.Start, w.Range.End,
            //    w.Certificates.Select(c => new CertificateResponse(c.Id, c.FilePath, c.Description)).ToList(),
            //    w.Range.UntilNow
            //)).ToList();
            //var educationResponses = src.Educations.Select(e => new EducationResponse(
            //    e.Id, e.Institution, e.FieldOfStudy, e.Degree.Name, e.AttendancePeriod.Start, e.AttendancePeriod.End,
            //    new CertificateResponse(e.EducationCertificate.Id, e.EducationCertificate.FilePath, e.EducationCertificate.Description),
            //    e.IsGraduated
            //)).ToList();
            var portfolioUrlResponses = src.PortfolioUrls.Select(p => p.Adapt<PortfolioUrlResponse>()).ToList();
            var socialMediaLinkResponses = src.SocialMediaLinks.Select(s => s.Adapt<SocialMediaLinkResponse>()).ToList();
            var workExperienceResponses = src.WorkExperiences.Select(we => we.Adapt<WorkExperienceResponse>()).ToList();
            var educationResponses = src.Educations.Select(e => e.Adapt<EducationResponse>()).ToList();

            return new SellerResponse(
                src.Id,
                src.PersonalDetails.FirstName,
                src.PersonalDetails.SecondName,
                src.PersonalDetails.LastName,
                src.PersonalDetails.Email,
                src.PersonalDetails.NationalNo,
                src.PersonalDetails.DateOfBirth,
                src.PersonalDetails.Address.Country,
                src.PersonalDetails.Address.City,
                src.PersonalDetails.Address.Region,
                portfolioUrlResponses,
                socialMediaLinkResponses,
                workExperienceResponses,
                educationResponses
            );
        }
        private static void ConfigMappingFromRequest(TypeAdapterConfig config)
        {
            ///// Add this configuration for IFormFile mapping
            //config.NewConfig<IFormFile, IFormFile>()
            //    .MapWith(src => src); // Just pass the reference directly

            //// Or if you need to ensure it's not null
            //config.NewConfig<IFormFile, IFormFile>()
            //    .MapWith(src => new FormFile(
            //        src.OpenReadStream(),
            //        0,
            //        src.Length,
            //        src.Name,
            //        src.FileName)
            //    {
            //        Headers = src.Headers,
            //        ContentType = src.ContentType,
            //        ContentDisposition = src.ContentDisposition
            //    });

            config.NewConfig<CreateOnlineSellerRequest, RegisterNewOnlineSellerCommand>()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.SecondName, src => src.SecondName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.NationalNo, src => src.NationalNo)
                .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
                .Map(dest => dest.Country, src => src.Country)
                .Map(dest => dest.City, src => src.City)
                .Map(dest => dest.Region, src => src.Region)
                .Map(dest => dest.WorkExperiences, src => src.WorkExperiences)
                .Map(dest => dest.Educations, src => src.Educations)
                .Map(dest => dest.PortfolioUrls, src => src.PortfolioUrls)
                .Map(dest => dest.SocialMediaLinks, src => src.SocialMediaLinks);


        }

    }
}
