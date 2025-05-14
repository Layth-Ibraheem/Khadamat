using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.Common.Interfcaes
{
    public interface IUnitOfWork
    {
        Task CommitChangesAsync();
    }
}
