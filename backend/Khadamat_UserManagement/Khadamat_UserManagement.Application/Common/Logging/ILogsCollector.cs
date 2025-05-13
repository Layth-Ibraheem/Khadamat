using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Common.Logging
{
    public interface ILogsCollector
    {
        void AddLog(Log log);
        Task CommitLogs();
    }

}
