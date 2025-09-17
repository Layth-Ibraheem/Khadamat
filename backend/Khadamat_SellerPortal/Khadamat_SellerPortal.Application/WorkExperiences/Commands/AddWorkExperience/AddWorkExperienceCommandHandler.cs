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

namespace Khadamat_SellerPortal.Application.WorkExperiences.Commands.AddWorkExperience
{
    public class AddWorkExperienceCommandHandler : IRequestHandler<AddWorkExperienceCommand, ErrorOr<Seller>>
    {
        private readonly ISender _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainFilesService _domainFilesService;
        public AddWorkExperienceCommandHandler(ISender mediator, IUnitOfWork unitOfWork, IDomainFilesService domainFilesService)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _domainFilesService = domainFilesService;
        }

        public async Task<ErrorOr<Seller>> Handle(AddWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var fetchtSellerResult = await _mediator.Send(new FetchSellerQuery(request.SellerId));
            if (fetchtSellerResult.IsError)
            {
                return fetchtSellerResult.FirstError;
            }
            var seller = fetchtSellerResult.Value;
            var addWorkExperienceResult = seller.AddWorkExperience(request.CompanyName, request.Start, request.End, request.Position, request.Field, request.UntilNow);
            if (addWorkExperienceResult.IsError)
            {
                return addWorkExperienceResult.FirstError;
            }
            foreach (var cert in request.Certificates)
            {
                string tempFilePath = await _domainFilesService.UploadFileToTempPath(cert.File);
                var addCertificates = seller.AddWorkExperienceCertification(
                    request.CompanyName,
                    request.Position,
                    tempFilePath,
                    cert.Description);
                if (addCertificates.IsError)
                {
                    return addCertificates.FirstError;
                }
            }
            await _unitOfWork.CommitChangesAsync(cancellationToken);
            return seller;
        }
    }
}
