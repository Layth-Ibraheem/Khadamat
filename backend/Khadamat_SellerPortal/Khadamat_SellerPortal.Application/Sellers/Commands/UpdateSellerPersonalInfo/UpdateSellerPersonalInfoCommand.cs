using ErrorOr;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.Sellers.Commands.UpdateOnlineSellerPersonalInfo
{
    public record UpdateSellerPersonalInfoCommand(
        int SellerId,
        string FirstName,
        string SecondName,
        string LastName,
        string Email,
        string? NationalNo,
        DateTime DateOfBirth,
        string Country,
        string City,
        string Region) : IRequest<ErrorOr<Seller>>;
}
