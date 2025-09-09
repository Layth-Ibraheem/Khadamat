using ErrorOr;
using Khadamat_SellerPortal.Domain.Common.Entities.EducationEntity;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.Educations.Commands.UpdateEducation
{
    public record UpdateEducationCommand(
        int SellerId,
        string Institution,
        string FieldOfStudy,
        EducationDegree Degree,
        bool IsGraduated,
        DateTime Start,
        DateTime? End) : IRequest<ErrorOr<Seller>>;
}
