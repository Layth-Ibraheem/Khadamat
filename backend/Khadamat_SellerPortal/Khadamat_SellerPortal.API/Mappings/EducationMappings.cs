using Mapster;
using DomainEducationDegree = Khadamat_SellerPortal.Domain.Common.Entities.EducationEntity.EducationDegree;
using APIEducationDegree = Khadamat_SellerPortal.Contracts.Educations.EducationDegree;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;
using Khadamat_SellerPortal.Contracts.Educations;
using Khadamat_SellerPortal.Application.Educations.Commands.AddEducation;
using Khadamat_SellerPortal.Domain.Common.Entities.EducationEntity;

namespace Khadamat_SellerPortal.API.Mappings
{
    public class EducationMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<APIEducationDegree, DomainEducationDegree>()
                .MapWith(src => DomainEducationDegree.FromValue((int)src));


            config.NewConfig<AddEducationRequest, EducationDto>()
                .Map(dest => dest.Institution, src => src.Institution)
                .Map(dest => dest.FieldOfStudy, src => src.FieldOfStudy)
                .Map(dest => dest.Degree, src => src.Degree)
                .Map(dest => dest.Start, src => src.Start)
                .Map(dest => dest.End, src => src.End)
                .Map(dest => dest.IsGraduated, src => src.IsGraduated)
                .Map(dest => dest.EducationCertificate, src => src.Certificate);

            config.NewConfig<Education, EducationResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Institution, src => src.Institution)
                .Map(dest => dest.FieldOfStudy, src => src.FieldOfStudy)
                .Map(dest => dest.Degree, src => src.Degree.Name)
                .Map(dest => dest.Start, src => src.AttendancePeriod.Start)
                .Map(dest => dest.End, src => src.AttendancePeriod.End)
                .Map(dest => dest.IsGraduated, src => src.IsGraduated)
                .Map(dest => dest.Certificate, src => src.EducationCertificate);

            config.NewConfig<AddEducationRequest, AddEducationCommand>();

        }
    }
}
