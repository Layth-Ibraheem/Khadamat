using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Application.Sellers.Commands.UpdateOnlineSellerPersonalInfo;
using Khadamat_SellerPortal.Application.Sellers.Queries.FetchSeller;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;

namespace Khadamat_SellerPortal.Application.Sellers.Commands.UpdateSellerPersonalInfo
{
    public class UpdateSellerPersonalInfoCommandHandler : IRequestHandler<UpdateSellerPersonalInfoCommand, ErrorOr<Seller>>
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly ISender _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateSellerPersonalInfoCommandHandler(ISellerRepository sellerRepository, ISender mediator, IUnitOfWork unitOfWork)
        {
            _sellerRepository = sellerRepository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Seller>> Handle(UpdateSellerPersonalInfoCommand request, CancellationToken cancellationToken)
        {
            var fetchSellerResult = await _mediator.Send(new FetchSellerQuery(request.SellerId));
            if (fetchSellerResult.IsError)
            {
                return fetchSellerResult.FirstError;
            }

            var seller = fetchSellerResult.Value;
            seller.UpdatePersonalInfo(
                request.FirstName,
                request.SecondName,
                request.LastName,
                request.Email,
                request.NationalNo,
                request.DateOfBirth,
                request.Country,
                request.City,
                request.Region);
            await _sellerRepository.UpdateSeller(seller);
            await _unitOfWork.CommitChangesAsync();
            return seller;
        }
    }
}
