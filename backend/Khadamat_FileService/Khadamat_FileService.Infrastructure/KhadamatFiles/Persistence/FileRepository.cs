using Khadamat_FileService.Application.Common.Interfaces;
using Khadamat_FileService.Domain.FileAggregate;
using Khadamat_FileService.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Infrastructure.KhadamatFiles.Persistence
{
    public class FileRepository : IFileRepository
    {
        private readonly Khadamat_FileServiceDbContext _context;

        public FileRepository(Khadamat_FileServiceDbContext context)
        {
            _context = context;
        }

        public async Task AddFile(KhadamatFile file)
        {
            await _context.KhadamatFiles.AddAsync(file);
        }

        public async Task DeleteFile(KhadamatFile file)
        {
            await Task.CompletedTask;
            _context.KhadamatFiles.Remove(file);
        }

        public async Task<KhadamatFile?> GetFileByPath(string path)
        {
            return await _context.KhadamatFiles.FirstOrDefaultAsync(x => x.Path == path);
        }

        public async Task UpdateFile(KhadamatFile file)
        {
            await Task.CompletedTask;
            _context.Update(file);
        }
    }
}
