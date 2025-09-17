using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Domain.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.ProfileImages.Commands.UploadProfileImage
{
    public class UploadProfileImageCommandHandler : IRequestHandler<UploadProfileImageCommand, ErrorOr<Success>>
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainFilesService _domainFilesService;

        public UploadProfileImageCommandHandler(IUnitOfWork unitOfWork, ISellerRepository sellerRepository, IDomainFilesService domainFilesService)
        {
            _unitOfWork = unitOfWork;
            _sellerRepository = sellerRepository;
            _domainFilesService = domainFilesService;
        }

        public async Task<ErrorOr<Success>> Handle(UploadProfileImageCommand request, CancellationToken cancellationToken)
        {
            var seller = await _sellerRepository.GetSellerById(request.SellerId);
            if (seller == null)
            {
                return Error.NotFound("Seller.NotFound", "There is no seller with the provided id");
            }
            // put 0 as Id because it will be updated later when the event handler (ProfileImageSaved) executed
            var uploadProfileImageResult = seller.AddProfileImage(0, await _domainFilesService.UploadFileToTempPath(request.ImageFile));
            if (uploadProfileImageResult.IsError)
            {
                return uploadProfileImageResult.FirstError;
            }
            return Result.Success;
        }
    }
}
