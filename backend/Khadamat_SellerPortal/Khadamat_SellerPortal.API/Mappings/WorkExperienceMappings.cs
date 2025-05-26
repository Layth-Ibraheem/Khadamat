using Khadamat_SellerPortal.Application.OnlineSellers.Common;
using Khadamat_SellerPortal.Contracts.WorkExperiences;
using Khadamat_SellerPortal.Domain.Common.Entities;
using Mapster;

namespace Khadamat_SellerPortal.API.Mappings
{
    public class WorkExperienceMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {


            config.NewConfig<AddWorkExperienceRequest, WorkExperienceDto>()
            .Map(dest => dest.CompanyName, src => src.CompanyName)
            .Map(dest => dest.Start, src => src.Start)
            .Map(dest => dest.End, src => src.End)
            .Map(dest => dest.UntilNow, src => src.UntilNow)
            .Map(dest => dest.Position, src => src.Position)
            .Map(dest => dest.Field, src => src.Field)
            .Map(dest => dest.Certificates, src => src.Certificates);

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

        }
    }
}
