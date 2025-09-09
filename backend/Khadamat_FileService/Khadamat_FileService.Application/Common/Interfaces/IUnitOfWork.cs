using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitChangesAsync(CancellationToken cancellationToken = default);
    }
}
