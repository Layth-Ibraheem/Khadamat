using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Application.OnlineSellers.Queries.FetchOnlineSeller;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.PortfolioURLs.Commands.DeletePortfolioUrl
{
    public class DeletePortfolioUrlCommandHandler : IRequestHandler<DeletePortfolioUrlCommand, ErrorOr<OnlineSeller>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISender _mediator;
        public DeletePortfolioUrlCommandHandler(IUnitOfWork unitOfWork, ISender mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<ErrorOr<OnlineSeller>> Handle(DeletePortfolioUrlCommand request, CancellationToken cancellationToken)
        {
            var fetchOnlineSellerResult = await _mediator.Send(new FetchOnlineSellerQuery(request.SellerId));
            if (fetchOnlineSellerResult.IsError)
            {
                return fetchOnlineSellerResult.FirstError;
            }
            var onlineSeller = fetchOnlineSellerResult.Value;
            var deletePortfolioRes = onlineSeller.DeletePortfolioUrl(request.Type);
            if (deletePortfolioRes.IsError)
            {
                return deletePortfolioRes.FirstError;
            }
            await _unitOfWork.CommitChangesAsync();
            return onlineSeller;
        }
    }
}
