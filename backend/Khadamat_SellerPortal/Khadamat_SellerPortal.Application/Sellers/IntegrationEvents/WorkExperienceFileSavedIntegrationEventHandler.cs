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
    public class WorkExperienceFileSavedIntegrationEventHandler : INotificationHandler<WorkExperienceFileSavedIntegrationEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISellerRepository _sellerRepository;
        private readonly IFileService _fileService;

        public WorkExperienceFileSavedIntegrationEventHandler(IFileService fileService, ISellerRepository sellerRepository, IUnitOfWork unitOfWork)
        {
            _fileService = fileService;
            _sellerRepository = sellerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(WorkExperienceFileSavedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var seller = await _sellerRepository.GetSellerByNationalNo(notification.NationalNo);
            if(seller != null)
            {
                var result = seller.UpdateWorkExperienceCertificate()
            }
        }
    }
}
