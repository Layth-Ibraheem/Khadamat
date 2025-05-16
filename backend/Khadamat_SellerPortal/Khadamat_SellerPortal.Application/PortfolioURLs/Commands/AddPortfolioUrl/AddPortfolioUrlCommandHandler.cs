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

namespace Khadamat_SellerPortal.Application.PortfolioURLs.Commands.AddPortfolioUrl
{
    public class AddPortfolioUrlCommandHandler : IRequestHandler<AddPortfolioUrlCommand, ErrorOr<OnlineSeller>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISender _mediator;
        public AddPortfolioUrlCommandHandler(ISender mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<OnlineSeller>> Handle(AddPortfolioUrlCommand request, CancellationToken cancellationToken)
        {
            var fetchOnlineSellerResult = await _mediator.Send(new FetchOnlineSellerQuery(request.SellerId));
            if (fetchOnlineSellerResult.IsError)
            {
                return fetchOnlineSellerResult.FirstError;
            }
            var onlineSeller = fetchOnlineSellerResult.Value;
            var addingPortfolioRes = onlineSeller.AddPortfolioUrl(request.Url, request.Type);
            if (addingPortfolioRes.IsError)
            {
                return addingPortfolioRes.FirstError;
            }
            await _unitOfWork.CommitChangesAsync();
            return onlineSeller;
        }
    }
}
