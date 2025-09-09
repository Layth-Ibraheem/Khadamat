using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Application.Sellers.Queries.FetchSeller;
using Khadamat_SellerPortal.Domain.Common.Interfaces;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.Educations.Commands.AddEducation
{
    public class AddEducationCommandHandler : IRequestHandler<AddEducationCommand, ErrorOr<Seller>>
    {
        private readonly ISender _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainFilesService _domainFilesService;
        public AddEducationCommandHandler(IUnitOfWork unitOfWork, ISender mediator, IDomainFilesService domainFilesService)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _domainFilesService = domainFilesService;
        }
        public async Task<ErrorOr<Seller>> Handle(AddEducationCommand request, CancellationToken cancellationToken)
        {
            var fetchtSellerResult = await _mediator.Send(new FetchSellerQuery(request.SellerId));
            if (fetchtSellerResult.IsError)
            {
                return fetchtSellerResult.FirstError;
            }
            var seller = fetchtSellerResult.Value;
            var addEducationResult = seller.AddEducation(request.Institution, request.FieldOfStudy, request.Degree, request.Start, request.End, request.IsGraduated);
            if (addEducationResult.IsError)
            {
                return addEducationResult.FirstError;
            }
            var addEducationCertificate = seller.AddEducationCertificate(
                request.Institution,
                request.FieldOfStudy,
                await _domainFilesService.UploadFileToTempPath(request.EducationCertificate.File),
                request.EducationCertificate.Description);

            if (addEducationCertificate.IsError)
            {
                return addEducationCertificate.FirstError;
            }
            await _unitOfWork.CommitChangesAsync();

            return seller;
        }
    }
}
