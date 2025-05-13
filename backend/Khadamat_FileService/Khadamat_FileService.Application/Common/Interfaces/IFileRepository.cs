using Khadamat_FileService.Domain.FileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Application.Common.Interfaces
{
    public interface IFileRepository
    {
        Task AddFile(KhadamatFile file);
        Task DeleteFile(KhadamatFile file);
        Task UpdateFile(KhadamatFile file);
        Task<KhadamatFile?> GetFileByPath(string path);
    }
}
