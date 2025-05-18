using Mapster;
using DomainPortfolioType = Khadamat_SellerPortal.Domain.Common.Entities.PortfolioUrlType;
using DomainSocialMediaLinkType = Khadamat_SellerPortal.Domain.Common.Entities.SocialMediaLinkType;
using DomainEducationDegree = Khadamat_SellerPortal.Domain.Common.Entities.EducationDegree;
using APIPortfolioType = Khadamat_SellerPortal.Contracts.OnlineSellers.PortfolioUrlType;
using APISocialMediaLinkType = Khadamat_SellerPortal.Contracts.OnlineSellers.SocialMediaLinkType;
using APIEducationDegree = Khadamat_SellerPortal.Contracts.OnlineSellers.EducationDegree;
using Khadamat_SellerPortal.Contracts.OnlineSellers;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;
using System.ComponentModel.DataAnnotations.Schema;
using Khadamat_SellerPortal.Domain.Common.Entities;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using Khadamat_SellerPortal.Application.OnlineSellers.Commands.RegisterOnlineSeller;
using Khadamat_SellerPortal.Application.Sellers.Commands.UpdateOnlineSellerPersonalInfo;


namespace Khadamat_SellerPortal.API.Mappings
{
    public class OnlineSellerMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            ConfigMappingFromRequest(config);

            ConfigMappingToResponse(config);

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


            config.NewConfig<APIPortfolioType, DomainPortfolioType>()
                .MapWith(src => DomainPortfolioType.FromValue((int)src));

            config.NewConfig<APISocialMediaLinkType, DomainSocialMediaLinkType>()
                .MapWith(src => DomainSocialMediaLinkType.FromValue((int)src));

            config.NewConfig<APIEducationDegree, DomainEducationDegree>()
                .MapWith(src => DomainEducationDegree.FromValue((int)src));

            config.NewConfig<AddCertificateRequest, CertificateDto>()
            .Map(dest => dest.FilePath, src => src.FilePath)
            .Map(dest => dest.Description, src => src.Description)
            .IgnoreNonMapped(true);

            config.NewConfig<AddPortfolioUrlRequest, PortfolioUrlDto>()
                .Map(dest => dest.Url, src => src.Url)
                .Map(dest => dest.Type, src => src.Type);

            config.NewConfig<AddSocialMediaLink, SocialMediaLinkDto>()
            .Map(dest => dest.Link, src => src.Link)
            .Map(dest => dest.Type, src => src.Type);

            config.NewConfig<AddWorkExperienceRequest, WorkExperienceDto>()
            .Map(dest => dest.CompanyName, src => src.CompanyName)
            .Map(dest => dest.Start, src => src.Start)
            .Map(dest => dest.End, src => src.End)
            .Map(dest => dest.UntilNow, src => src.UntilNow)
            .Map(dest => dest.Position, src => src.Position)
            .Map(dest => dest.Field, src => src.Field)
            .Map(dest => dest.Certificates, src => src.Certificates);

            config.NewConfig<AddEducationRequest, EducationDto>()
                .Map(dest => dest.Institution, src => src.Institution)
                .Map(dest => dest.FieldOfStudy, src => src.FieldOfStudy)
                .Map(dest => dest.Degree, src => src.Degree)
                .Map(dest => dest.Start, src => src.Start)
                .Map(dest => dest.End, src => src.End)
                .Map(dest => dest.IsGraduated, src => src.IsGraduated)
                .Map(dest => dest.EducationCertificate, src => src.Certificate);

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


            config.NewConfig<UpdateSellerPersonalInfoRequest, UpdateSellerPersonalInfoCommand>()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.SecondName, src => src.SecondName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.NationalNo, src => src.NationalNo)
                .Map(dest => dest.DateOfBirth, src => src.DateOfBirth)
                .Map(dest => dest.Country, src => src.Country)
                .Map(dest => dest.City, src => src.City)
                .Map(dest => dest.Region, src => src.Region)
                .IgnoreNonMapped(true);
        }

        private static void ConfigMappingToResponse(TypeAdapterConfig config)
        {

            // Certificate to CertificateResponse
            config.NewConfig<Certificate, CertificateResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FilePath, src => src.FilePath)
                .Map(dest => dest.Description, src => src.Description);

            // Education to EducationResponse
            config.NewConfig<Education, EducationResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Institution, src => src.Institution)
                .Map(dest => dest.FieldOfStudy, src => src.FieldOfStudy)
                .Map(dest => dest.Degree, src => src.Degree.Name)
                .Map(dest => dest.Start, src => src.AttendancePeriod.Start)
                .Map(dest => dest.End, src => src.AttendancePeriod.End)
                .Map(dest => dest.IsGraduated, src => src.IsGraduated)
                .Map(dest => dest.Certificate, src => src.EducationCertificate);

            // WorkExperience to WorkExperienceResponse
            config.NewConfig<WorkExperience, WorkExperienceResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.CompanyName, src => src.CompanyName)
                .Map(dest => dest.Position, src => src.Position)
                .Map(dest => dest.Field, src => src.Field)
                .Map(dest => dest.Start, src => src.Range.Start)
                .Map(dest => dest.End, src => src.Range.End)
                .Map(dest => dest.UntilNow, src => src.Range.UntilNow)
                .Map(dest => dest.Certificates, src => src.Certificates);


            // SocialMediaLink to SocialMediaLinkResponse
            config.NewConfig<SocialMediaLink, SocialMediaLinkResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Link, src => src.Link)
                .Map(dest => dest.Type, src => src.Type.Name);


            // PortfolioUrl to PortfolioUrlResponse
            config.NewConfig<PortfolioUrl, PortfolioUrlResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Url, src => src.Url)
                .Map(dest => dest.Type, src => src.Type.Name);

            // Main OnlineSeller to OnlineSellerResponse mapping
            config.NewConfig<OnlineSeller, OnlineSellerResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FirstName, src => src.PersonalDetails.FirstName)
                .Map(dest => dest.SecondName, src => src.PersonalDetails.SecondName)
                .Map(dest => dest.LastName, src => src.PersonalDetails.LastName)
                .Map(dest => dest.Email, src => src.PersonalDetails.Email)
                .Map(dest => dest.NationalNo, src => src.PersonalDetails.NationalNo)
                .Map(dest => dest.DateOfBirth, src => src.PersonalDetails.DateOfBirth)
                .Map(dest => dest.Country, src => src.PersonalDetails.Address.Country)
                .Map(dest => dest.City, src => src.PersonalDetails.Address.City)
                .Map(dest => dest.Region, src => src.PersonalDetails.Address.Region)
                .Map(dest => dest.PortfolioUrls, src => src.PortfolioUrls)
                .Map(dest => dest.SocialMediaLinks, src => src.SocialMediaLinks)
                .Map(dest => dest.WorkExperiences, src => src.WorkExperiences)
                .Map(dest => dest.Educations, src => src.Educations);
        }
    }
}
