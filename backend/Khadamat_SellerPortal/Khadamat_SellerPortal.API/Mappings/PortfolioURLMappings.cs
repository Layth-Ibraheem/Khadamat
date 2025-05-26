using Mapster;
using DomainPortfolioType = Khadamat_SellerPortal.Domain.Common.Entities.PortfolioUrlType;
using APIPortfolioType = Khadamat_SellerPortal.Contracts.PortfolioURLs.PortfolioUrlType;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;
using Khadamat_SellerPortal.Contracts.PortfolioURLs;
using Khadamat_SellerPortal.Domain.Common.Entities;

namespace Khadamat_SellerPortal.API.Mappings
{
    public class PortfolioURLMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<APIPortfolioType, DomainPortfolioType>()
                .MapWith(src => DomainPortfolioType.FromValue((int)src));


            config.NewConfig<AddPortfolioUrlRequest, PortfolioUrlDto>()
                .Map(dest => dest.Url, src => src.Url)
                .Map(dest => dest.Type, src => src.Type);


            // PortfolioUrl to PortfolioUrlResponse
            config.NewConfig<PortfolioUrl, PortfolioUrlResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Url, src => src.Url)
                .Map(dest => dest.Type, src => src.Type.Name);

        }
    }
}
