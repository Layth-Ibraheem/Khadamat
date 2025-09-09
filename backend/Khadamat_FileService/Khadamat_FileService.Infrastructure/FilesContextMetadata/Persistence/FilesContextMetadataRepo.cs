using Khadamat_FileService.Application.Common.FileContext;
using Khadamat_FileService.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Infrastructure.FilesContextMetadata.Persistence
{
    public class FilesContextMetadataRepo : IFilesContextMetadataRepo
    {
        private readonly Khadamat_FileServiceDbContext _context;
        public FilesContextMetadataRepo(Khadamat_FileServiceDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(FileContextMetadata fileContextMetadata)
        {
            await _context.FilesContextMetadata.AddAsync(fileContextMetadata);
        }

        public async Task DeleteAsync(string path)
        {
            await _context.FilesContextMetadata.Where(f => f.FilePath == path).ExecuteDeleteAsync();
        }

        public async Task<FileContextMetadata?> GetByPathAsync(string path)
        {
            return await _context.FilesContextMetadata.FirstOrDefaultAsync(f => f.FilePath == path);
        }
    }
}
