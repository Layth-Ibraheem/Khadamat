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

namespace Khadamat_SellerPortal.Application.SocialMediaLinks.Commands.UpdateSocialMediaLink
{
    public class UpdateSocialMediaLinkCommandHandler : IRequestHandler<UpdateSocialMediaLinkCommand, ErrorOr<Seller>>
    {
        private readonly ISender _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateSocialMediaLinkCommandHandler(ISender mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Seller>> Handle(UpdateSocialMediaLinkCommand request, CancellationToken cancellationToken)
        {
            var query = new FetchSellerQuery(request.SellerId);
            var fetchSellerResult = await _mediator.Send(query);
            if (fetchSellerResult.IsError)
            {
                return fetchSellerResult.FirstError;
            }
            var seller = fetchSellerResult.Value;

            var updateSocialMediaLinkResult = seller.UpdateSocialMediaLinkForType(request.Type, request.Link);
            if (updateSocialMediaLinkResult.IsError)
            {
                return updateSocialMediaLinkResult.FirstError;
            }

            return seller;
        }
    }
}
