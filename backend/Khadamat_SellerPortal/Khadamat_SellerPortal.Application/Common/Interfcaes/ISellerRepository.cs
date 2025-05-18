using Khadamat_SellerPortal.Domain.SellerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.Common.Interfcaes
{
    public interface ISellerRepository
    {
        Task AddSeller(Seller seller);
        Task UpdateSeller(Seller seller);
        Task<Seller?> GetSellerById(int id);

    }
}
