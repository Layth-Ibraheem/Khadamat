using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Application.OnlineSellers.Commands.UpdateOnlineSeller;
using Khadamat_SellerPortal.Application.OnlineSellers.Queries.FetchOnlineSeller;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.OnlineSellers.Commands.UpdateOnlineSellerPersonalInfo
{
    public class UpdateOnlineSellerPersonalInfoCommandHandler : IRequestHandler<UpdateOnlineSellerPersonalInfoCommand, ErrorOr<OnlineSeller>>
    {
        private readonly IOnlineSellerRepository _onlineSellerRepository;
        private readonly ISender _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateOnlineSellerPersonalInfoCommandHandler(IOnlineSellerRepository onlineSellerRepository, ISender mediator, IUnitOfWork unitOfWork)
        {
            _onlineSellerRepository = onlineSellerRepository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<OnlineSeller>> Handle(UpdateOnlineSellerPersonalInfoCommand request, CancellationToken cancellationToken)
        {
            var fetchOnlineSellerResult = await _mediator.Send(new FetchOnlineSellerQuery(request.SellerId));
            if (fetchOnlineSellerResult.IsError)
            {
                return fetchOnlineSellerResult.FirstError;
            }

            var onlineSeller = fetchOnlineSellerResult.Value;
            onlineSeller.UpdatePersonalInfo(
                request.FirstName,
                request.SecondName,
                request.LastName,
                request.Email,
                request.NationalNo,
                request.DateOfBirth,
                request.Country,
                request.City,
                request.Region);
            _onlineSellerRepository.UpdateOnlineSellerProfile(onlineSeller);
            await _unitOfWork.CommitChangesAsync();
            return onlineSeller;
        }
    }
}
