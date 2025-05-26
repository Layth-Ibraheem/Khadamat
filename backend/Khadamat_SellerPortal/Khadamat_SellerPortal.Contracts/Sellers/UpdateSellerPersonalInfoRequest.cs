using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Contracts.Sellers
{
    public record UpdateSellerPersonalInfoRequest(
        string FirstName,
        string SecondName,
        string LastName,
        string Email,
        string? NationalNo,
        DateTime DateOfBirth,
        string Country,
        string City,
        string Region);
}
