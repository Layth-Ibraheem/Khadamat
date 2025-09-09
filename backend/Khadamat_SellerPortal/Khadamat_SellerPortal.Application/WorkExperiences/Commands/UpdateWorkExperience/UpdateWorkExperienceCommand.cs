using ErrorOr;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.WorkExperiences.Commands.UpdateWorkExperience
{
    public record UpdateWorkExperienceCommand(
        int SellerId,
        string CompanyName,
        DateTime Start,
        DateTime? End,
        bool UntilNow,
        string Position,
        string Field) : IRequest<ErrorOr<Seller>>;
}
