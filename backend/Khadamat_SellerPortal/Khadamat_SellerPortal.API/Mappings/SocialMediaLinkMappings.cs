
using Mapster;
using DomainSocialMediaLinkType = Khadamat_SellerPortal.Domain.Common.Entities.SocialMediaLinkEntity.SocialMediaLinkType;
using APISocialMediaLinkType = Khadamat_SellerPortal.Contracts.SocialMediaLinks.SocialMediaLinkType;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;
using Khadamat_SellerPortal.Contracts.SocialMediaLinks;
using Khadamat_SellerPortal.Domain.Common.Entities.SocialMediaLinkEntity;
namespace Khadamat_SellerPortal.API.Mappings
{
    public class SocialMediaLinkMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {

            config.NewConfig<APISocialMediaLinkType, DomainSocialMediaLinkType>()
                .MapWith(src => DomainSocialMediaLinkType.FromValue((int)src));

            config.NewConfig<AddSocialMediaLinkRequest, SocialMediaLinkDto>()
            .Map(dest => dest.Link, src => src.Link)
            .Map(dest => dest.Type, src => src.Type);

            config.NewConfig<SocialMediaLink, SocialMediaLinkResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Link, src => src.Link)
                .Map(dest => dest.Type, src => src.Type.Name);

        }
    }
}
