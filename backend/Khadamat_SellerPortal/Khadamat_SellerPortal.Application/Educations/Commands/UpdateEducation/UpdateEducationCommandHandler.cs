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

namespace Khadamat_SellerPortal.Application.Educations.Commands.UpdateEducation
{
    public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, ErrorOr<Seller>>
    {
        private readonly ISender _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateEducationCommandHandler(ISender mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Seller>> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            var fetchtSellerResult = await _mediator.Send(new FetchSellerQuery(request.SellerId));
            if (fetchtSellerResult.IsError)
            {
                return fetchtSellerResult.FirstError;
            }
            var seller = fetchtSellerResult.Value;

            var updateEducationResult = seller.UpdateEducation(request.Institution, request.FieldOfStudy, request.Degree, request.Start, request.End, request.IsGraduated);
            if (updateEducationResult.IsError)
            {
                return updateEducationResult.FirstError;
            }
            await _unitOfWork.CommitChangesAsync();
            return seller;

        }
    }
}
