using ErrorOr;
using Khadamat_SellerPortal.Domain.Common.Entities.SocialMediaLinkEntity;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.SocialMediaLinks.Commands.DeleteSocialMediaLink
{
    public record DeleteSocialMediaLinkCommand(int SellerId, SocialMediaLinkType Type) : IRequest<ErrorOr<Seller>>;

}
