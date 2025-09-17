using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Application.Sellers.Queries.FetchSeller;
using Khadamat_SellerPortal.Application.Sellers.Queries.FetchSellerByNationalNo;
using Khadamat_SharedKernal.Khadamat_FilesService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.Sellers.IntegrationEvents
{
    public class EducationFileSavedIntegrationEventHandler : INotificationHandler<EducationFileSavedIntegrationEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISellerRepository _sellerRepository;
        private readonly IFileService _fileService;
        public EducationFileSavedIntegrationEventHandler(IUnitOfWork unitOfWork, ISellerRepository sellerRepository, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _sellerRepository = sellerRepository;
            _fileService = fileService;
        }

        public async Task Handle(EducationFileSavedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            
            var seller = await _sellerRepository.GetSellerById(notification.SellerId);
            if(seller != null)
            {
                var result = seller.UpdateEducationCertificate(notification.EducationId,
                                                                                           notification.FullPath,
                                                                                           notification.FileId,
                                                                                           out string previousPath);
                if (result.IsError)
                {
                    return;
                    // log the error
                }
                await _fileService.DeleteTempFile(previousPath);
                await _sellerRepository.UpdateSeller(seller);
                await _unitOfWork.CommitChangesAsync();
            }
            // log that the seller is null, maybe it is deleted

        }
    }
}
