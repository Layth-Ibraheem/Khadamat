using Khadamat_FileService.Domain.Common.Interfaces;
using Khadamat_FileService.Domain.FileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Infrastructure.Common.EntityTag
{
    public class EntityTagGenerator : IEntityTagGenerator
    {
        public string GenerateETag(KhadamatFile file)
        {
            // I did this implementation just for learning purposes, to lear how to hash a stream and ...
            // I can use the StoredFileName as the ETAG as it is unique (GUID)
            try
            {
                using var stream = File.OpenRead(file.Path);
                var hash = SHA256.HashData(stream);
                return $"\"{Convert.ToHexString(hash)}\"";
            }
            catch
            {
                var content = $"{file.OriginalFileName}-{file.SizeInBytes}-{file.UploadDate.Ticks}";
                var contentBytes = Encoding.UTF8.GetBytes(content);
                var hash = SHA256.HashData(contentBytes);
                return $"\"{Convert.ToHexString(hash)}\"";
            }
        }
    }
}
