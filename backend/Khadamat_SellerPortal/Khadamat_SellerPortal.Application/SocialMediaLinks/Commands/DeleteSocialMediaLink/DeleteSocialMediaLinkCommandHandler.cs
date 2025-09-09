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

namespace Khadamat_SellerPortal.Application.SocialMediaLinks.Commands.DeleteSocialMediaLink
{
    public class DeleteSocialMediaLinkCommandHandler : IRequestHandler<DeleteSocialMediaLinkCommand, ErrorOr<Seller>>
    {
        private readonly ISender _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteSocialMediaLinkCommandHandler(ISender mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Seller>> Handle(DeleteSocialMediaLinkCommand request, CancellationToken cancellationToken)
        {
            var query = new FetchSellerQuery(request.SellerId);
            var fetchSellerResult = await _mediator.Send(query);
            if (fetchSellerResult.IsError)
            {
                return fetchSellerResult.FirstError;
            }
            var seller = fetchSellerResult.Value;

            var deleteSocialMediaLinkResult = seller.DeleteSocialMediaLink(request.Type);
            if (deleteSocialMediaLinkResult.IsError)
            {
                return deleteSocialMediaLinkResult.FirstError;
            }
            await _unitOfWork.CommitChangesAsync();
            return seller;
        }
    }
}
