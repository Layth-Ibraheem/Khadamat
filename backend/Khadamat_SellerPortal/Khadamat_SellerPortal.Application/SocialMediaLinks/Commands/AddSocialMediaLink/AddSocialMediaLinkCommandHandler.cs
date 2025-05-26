using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Application.Sellers.Queries.FetchSeller;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.SocialMediaLinks.Commands.AddSocialMediaLink
{
    public class AddSocialMediaLinkCommandHandler : IRequestHandler<AddSocialMediaLinkCommand, ErrorOr<Seller>>
    {
        private readonly ISender _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public AddSocialMediaLinkCommandHandler(ISender mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Seller>> Handle(AddSocialMediaLinkCommand request, CancellationToken cancellationToken)
        {
            var query = new FetchSellerQuery(request.SellerId);
            var fetchSellerResult = await _mediator.Send(query);
            if (fetchSellerResult.IsError)
            {
                return fetchSellerResult.FirstError;
            }
            var seller = fetchSellerResult.Value;

            var addSocialMediaLinkResult = seller.AddSocialMediaLink(request.Type, request.Link);
            if (addSocialMediaLinkResult.IsError)
            {
                return addSocialMediaLinkResult.FirstError;
            }

            return seller;

        }
    }
}
