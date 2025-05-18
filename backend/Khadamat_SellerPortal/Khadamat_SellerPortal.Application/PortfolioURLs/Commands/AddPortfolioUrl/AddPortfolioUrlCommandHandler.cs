using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Application.Sellers.Queries.FetchSeller;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;

namespace Khadamat_SellerPortal.Application.PortfolioURLs.Commands.AddPortfolioUrl
{
    public class AddPortfolioUrlCommandHandler : IRequestHandler<AddPortfolioUrlCommand, ErrorOr<Seller>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISender _mediator;
        public AddPortfolioUrlCommandHandler(ISender mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Seller>> Handle(AddPortfolioUrlCommand request, CancellationToken cancellationToken)
        {
            var fetchSellerResult = await _mediator.Send(new FetchSellerQuery(request.SellerId));
            if (fetchSellerResult.IsError)
            {
                return fetchSellerResult.FirstError;
            }
            var seller = fetchSellerResult.Value;
            var addingPortfolioRes = seller.AddPortfolioUrl(request.Url, request.Type);
            if (addingPortfolioRes.IsError)
            {
                return addingPortfolioRes.FirstError;
            }
            await _unitOfWork.CommitChangesAsync();
            return seller;
        }
    }
}
