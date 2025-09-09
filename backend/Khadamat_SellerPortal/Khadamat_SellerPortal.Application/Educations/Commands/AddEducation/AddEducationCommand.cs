using ErrorOr;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;
using Khadamat_SellerPortal.Domain.Common.Entities.EducationEntity;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.Educations.Commands.AddEducation
{
    public record AddEducationCommand(
        int SellerId,
        string Institution,
        string FieldOfStudy,
        EducationDegree Degree,
        DateTime Start,
        DateTime? End,
        CertificateDto EducationCertificate,
        bool IsGraduated = false) : IRequest<ErrorOr<Seller>>;
}
