using Khadamat_SellerPortal.Application.Sellers.Commands.UpdateOnlineSellerPersonalInfo;
using Khadamat_SellerPortal.Contracts.OnlineSellers;
using Khadamat_SellerPortal.Contracts.Sellers;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using Mapster;

namespace Khadamat_SellerPortal.API.Mappings
{
    public class SellerMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {


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

            config.NewConfig<Seller, SellerResponse>()
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
