using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Application.Sellers.Queries.FetchSeller;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;

namespace Khadamat_SellerPortal.Application.PortfolioURLs.Commands.DeletePortfolioUrl
{
    public class DeletePortfolioUrlCommandHandler : IRequestHandler<DeletePortfolioUrlCommand, ErrorOr<Seller>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISender _mediator;
        public DeletePortfolioUrlCommandHandler(IUnitOfWork unitOfWork, ISender mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<ErrorOr<Seller>> Handle(DeletePortfolioUrlCommand request, CancellationToken cancellationToken)
        {
            var fetchSellerResult = await _mediator.Send(new FetchSellerQuery(request.SellerId));
            if (fetchSellerResult.IsError)
            {
                return fetchSellerResult.FirstError;
            }
            var seller = fetchSellerResult.Value;
            var deletePortfolioRes = seller.DeletePortfolioUrl(request.Type);
            if (deletePortfolioRes.IsError)
            {
                return deletePortfolioRes.FirstError;
            }
            await _unitOfWork.CommitChangesAsync();
            return seller;
        }
    }
}
