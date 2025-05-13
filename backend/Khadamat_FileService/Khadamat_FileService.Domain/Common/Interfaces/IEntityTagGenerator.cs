using Khadamat_FileService.Domain.FileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Domain.Common.Interfaces
{
    public interface IEntityTagGenerator
    {
        string GenerateETag(KhadamatFile file);
    }
}
