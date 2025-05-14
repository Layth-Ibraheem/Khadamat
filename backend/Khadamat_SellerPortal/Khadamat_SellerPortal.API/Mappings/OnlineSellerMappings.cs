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
using Khadamat_SellerPortal.Application.OnlineSellers.Commands.UpdateOnlineSeller;


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
            .Map(dest => dest.FilePath, src => src.filePath)
            .Map(dest => dest.Description, src => src.description)
            .IgnoreNonMapped(true);

            config.NewConfig<AddPortfolioUrlRequest, PortfolioUrlDto>()
                .Map(dest => dest.Url, src => src.url)
                .Map(dest => dest.Type, src => src.type);

            config.NewConfig<AddSocialMediaLink, SocialMediaLinkDto>()
            .Map(dest => dest.Link, src => src.link)
            .Map(dest => dest.Type, src => src.type);

            config.NewConfig<AddWorkExperienceRequest, WorkExperienceDto>()
            .Map(dest => dest.CompanyName, src => src.companyName)
            .Map(dest => dest.Start, src => src.start)
            .Map(dest => dest.End, src => src.end)
            .Map(dest => dest.UntilNow, src => src.untilNow)
            .Map(dest => dest.Position, src => src.position)
            .Map(dest => dest.Field, src => src.field)
            .Map(dest => dest.Certificates, src => src.certificates);

            config.NewConfig<AddEducationRequest, EducationDto>()
                .Map(dest => dest.Institution, src => src.institution)
                .Map(dest => dest.FieldOfStudy, src => src.fieldOfStudy)
                .Map(dest => dest.Degree, src => src.degree)
                .Map(dest => dest.Start, src => src.start)
                .Map(dest => dest.End, src => src.end)
                .Map(dest => dest.IsGraduated, src => src.isGraduated)
                .Map(dest => dest.EducationCertificate, src => src.certificate);

            config.NewConfig<CreateOnlineSellerRequest, RegisterNewOnlineSellerCommand>()
                .Map(dest => dest.FirstName, src => src.firstName)
                .Map(dest => dest.SecondName, src => src.secondName)
                .Map(dest => dest.LastName, src => src.lastName)
                .Map(dest => dest.Email, src => src.email)
                .Map(dest => dest.NationalNo, src => src.nationalNo)
                .Map(dest => dest.DateOfBirth, src => src.dateOfBirth)
                .Map(dest => dest.Country, src => src.country)
                .Map(dest => dest.City, src => src.city)
                .Map(dest => dest.Region, src => src.region)
                .Map(dest => dest.WorkExperiences, src => src.workExperiences)
                .Map(dest => dest.Educations, src => src.educations)
                .Map(dest => dest.PortfolioUrls, src => src.portfolioUrls)
                .Map(dest => dest.SocialMediaLinks, src => src.socialMediaLinks);


            config.NewConfig<UpdateSellerPersonalInfoRequest, UpdateOnlineSellerPersonalInfoCommand>()
                .Map(dest => dest.FirstName, src => src.firstName)
                .Map(dest => dest.SecondName, src => src.secondName)
                .Map(dest => dest.LastName, src => src.lastName)
                .Map(dest => dest.Email, src => src.email)
                .Map(dest => dest.NationalNo, src => src.nationalNo)
                .Map(dest => dest.DateOfBirth, src => src.dateOfBirth)
                .Map(dest => dest.Country, src => src.country)
                .Map(dest => dest.City, src => src.city)
                .Map(dest => dest.Region, src => src.region)
                .IgnoreNonMapped(true);
        }

        private static void ConfigMappingToResponse(TypeAdapterConfig config)
        {

            // Certificate to CertificateResponse
            config.NewConfig<Certificate, CertificateResponse>()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.filePath, src => src.FilePath)
                .Map(dest => dest.description, src => src.Description);

            // Education to EducationResponse
            config.NewConfig<Education, EducationResponse>()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.Institution, src => src.Institution)
                .Map(dest => dest.fieldOfStudy, src => src.FieldOfStudy)
                .Map(dest => dest.degree, src => src.Degree.Name)
                .Map(dest => dest.start, src => src.AttendancePeriod.Start)
                .Map(dest => dest.end, src => src.AttendancePeriod.End)
                .Map(dest => dest.isGraduated, src => src.IsGraduated)
                .Map(dest => dest.certificate, src => src.EducationCertificate);

            // WorkExperience to WorkExperienceResponse
            config.NewConfig<WorkExperience, WorkExperienceResponse>()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.companyName, src => src.CompanyName)
                .Map(dest => dest.position, src => src.Position)
                .Map(dest => dest.field, src => src.Field)
                .Map(dest => dest.start, src => src.Range.Start)
                .Map(dest => dest.end, src => src.Range.End)
                .Map(dest => dest.untilNow, src => src.Range.UntilNow)
                .Map(dest => dest.certificates, src => src.Certificates);


            // SocialMediaLink to SocialMediaLinkResponse
            config.NewConfig<SocialMediaLink, SocialMediaLinkResponse>()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.link, src => src.Link)
                .Map(dest => dest.type, src => src.Type.Name);


            // PortfolioUrl to PortfolioUrlResponse
            config.NewConfig<PortfolioUrl, PortfolioUrlResponse>()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.Url, src => src.Url)
                .Map(dest => dest.type, src => src.Type.Name);

            // Main OnlineSeller to OnlineSellerResponse mapping
            config.NewConfig<OnlineSeller, OnlineSellerResponse>()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.firstName, src => src.PersonalDetails.FirstName)
                .Map(dest => dest.secondName, src => src.PersonalDetails.SecondName)
                .Map(dest => dest.lastName, src => src.PersonalDetails.LastName)
                .Map(dest => dest.email, src => src.PersonalDetails.Email)
                .Map(dest => dest.nationalNo, src => src.PersonalDetails.NationalNo)
                .Map(dest => dest.dateOfBirth, src => src.PersonalDetails.DateOfBirth)
                .Map(dest => dest.country, src => src.PersonalDetails.Address.Country)
                .Map(dest => dest.city, src => src.PersonalDetails.Address.City)
                .Map(dest => dest.region, src => src.PersonalDetails.Address.Region)
                .Map(dest => dest.portfolioUrls, src => src.PortfolioUrls)
                .Map(dest => dest.socialMediaLinks, src => src.SocialMediaLinks)
                .Map(dest => dest.workExperiences, src => src.WorkExperiences)
                .Map(dest => dest.educations, src => src.Educations);
        }
    }
}
