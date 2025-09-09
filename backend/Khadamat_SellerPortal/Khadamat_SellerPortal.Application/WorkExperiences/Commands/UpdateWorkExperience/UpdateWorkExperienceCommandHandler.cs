using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Application.Sellers.Queries.FetchSeller;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.WorkExperiences.Commands.UpdateWorkExperience
{
    internal class UpdateWorkExperienceCommandHandler : IRequestHandler<UpdateWorkExperienceCommand, ErrorOr<Seller>>
    {
        private readonly ISender _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkExperienceCommandHandler(ISender mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Seller>> Handle(UpdateWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var fetchSellerResult = await _mediator.Send(new FetchSellerQuery(request.SellerId));
            if (fetchSellerResult.IsError)
            {
                return fetchSellerResult.FirstError;
            }
            var seller = fetchSellerResult.Value;
            var updateWorkExperienceResult = seller.UpdateWorkExperience(request.CompanyName, request.Position, request.Field, request.Start, request.End, request.UntilNow);
            if (updateWorkExperienceResult.IsError)
            {
                return updateWorkExperienceResult.FirstError;
            }
            await _unitOfWork.CommitChangesAsync();
            return seller;
        }
    }
}
