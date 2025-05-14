using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.Common.Interfcaes
{
    public interface IOnlineSellerRepository
    {
        Task AddOnlineSeller(OnlineSeller onlineSeller);
        void UpdateOnlineSellerProfile(OnlineSeller onlineSeller);
        Task<OnlineSeller?> GetOnlineSellerById(int id);
    }
}
