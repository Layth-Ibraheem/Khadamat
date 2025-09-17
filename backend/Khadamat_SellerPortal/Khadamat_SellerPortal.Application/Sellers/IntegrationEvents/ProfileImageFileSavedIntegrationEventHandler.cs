using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SharedKernal.Khadamat_FilesService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.Sellers.IntegrationEvents
{
    public class ProfileImageFileSavedIntegrationEventHandler : INotificationHandler<ProfileImageFileSavedIntegrationEvent>
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        public ProfileImageFileSavedIntegrationEventHandler(ISellerRepository sellerRepository, IUnitOfWork unitOfWork, IFileService fileService)
        {
            _sellerRepository = sellerRepository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task Handle(ProfileImageFileSavedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var seller = await _sellerRepository.GetSellerById(notification.SellerId);

            var updateProfileImageMetadataRes = seller!.UpdateProfileImage(notification.FileId, notification.FullPath, out var previousPath);
            if (updateProfileImageMetadataRes.IsError)
            {
                // log the error
                return;
            }
            await _fileService.DeleteTempFile(previousPath);
            await _sellerRepository.UpdateSeller(seller);
            await _unitOfWork.CommitChangesAsync(cancellationToken);


        }
    }
}
