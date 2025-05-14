using ErrorOr;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.OnlineSellers.Commands.UpdateOnlineSeller
{
    public record UpdateOnlineSellerPersonalInfoCommand(
        int SellerId,
        string FirstName,
        string SecondName,
        string LastName,
        string Email,
        string? NationalNo,
        DateTime DateOfBirth,
        string Country,
        string City,
        string Region): IRequest<ErrorOr<OnlineSeller>>;
}
