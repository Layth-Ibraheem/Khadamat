using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Domain.Common
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot(int id = 0) : base(id)
        {
        }
        protected AggregateRoot() { }
    }
}
