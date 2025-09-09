using Khadamat_SellerPortal.Application.OnlineSellers.Common;
using Khadamat_SellerPortal.Contracts.Certificates;
using Khadamat_SellerPortal.Domain.Common.Entities.CertificateEntity;
using Mapster;

namespace Khadamat_SellerPortal.API.Mappings
{
    public class CertificateMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {

            config.NewConfig<AddCertificateRequest, CertificateDto>()
            .Map(dest => dest.FilePath, src => src.FilePath)
            .Map(dest => dest.Description, src => src.Description)
            .IgnoreNonMapped(true);


            // Certificate to CertificateResponse
            config.NewConfig<Certificate, CertificateResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FilePath, src => src.CachedFilePath)
                .Map(dest => dest.Description, src => src.Description);

        }
    }
}
