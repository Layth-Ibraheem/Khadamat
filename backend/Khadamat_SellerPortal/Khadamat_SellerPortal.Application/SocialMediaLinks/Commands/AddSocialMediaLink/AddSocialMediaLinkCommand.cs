using ErrorOr;
using Khadamat_SellerPortal.Domain.Common.Entities;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.SocialMediaLinks.Commands.AddSocialMediaLink
{
    public record AddSocialMediaLinkCommand(int SellerId, SocialMediaLinkType Type, string Link) : IRequest<ErrorOr<Seller>>;
}
