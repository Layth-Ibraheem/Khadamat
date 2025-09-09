using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Application.Common.FileContext
{
    public interface IFilesContextMetadataRepo
    {
        Task AddAsync(FileContextMetadata fileContextMetadata);
        Task<FileContextMetadata?> GetByPathAsync(string path);
        Task DeleteAsync(string path);
    }
}
