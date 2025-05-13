using Khadamat_UserManagement.Domain.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Domain.RegisterApplicationAggregate.Events
{
    public record ApplicationCreatedDomainEvent(string username, string email) : ICriticalDomainEvent;
}
