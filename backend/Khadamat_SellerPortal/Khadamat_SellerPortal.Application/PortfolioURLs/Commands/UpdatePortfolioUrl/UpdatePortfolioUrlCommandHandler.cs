using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Application.Sellers.Queries.FetchSeller;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;

namespace Khadamat_SellerPortal.Application.PortfolioURLs.Commands.UpdatePortfolioUrl
{
    public class UpdatePortfolioUrlCommandHandler : IRequestHandler<UpdatePortfolioUrlCommand, ErrorOr<Seller>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISender _mediator;

        public UpdatePortfolioUrlCommandHandler(IUnitOfWork unitOfWork, ISender mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<ErrorOr<Seller>> Handle(UpdatePortfolioUrlCommand request, CancellationToken cancellationToken)
        {
            var fetchSellerResult = await _mediator.Send(new FetchSellerQuery(request.SellerId));
            if (fetchSellerResult.IsError)
            {
                return fetchSellerResult.FirstError;
            }
            var seller = fetchSellerResult.Value;
            var updatePortfolioRes = seller.UpdatePortfolioUrlForType(request.Type, request.Url);
            if (updatePortfolioRes.IsError)
            {
                return updatePortfolioRes.FirstError;
            }
            await _unitOfWork.CommitChangesAsync();
            return (OnlineSeller)seller;
        }
    }
}
