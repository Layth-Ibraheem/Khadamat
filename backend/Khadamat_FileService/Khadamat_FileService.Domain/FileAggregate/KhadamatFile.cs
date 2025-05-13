using Khadamat_FileService.Domain.Common;
using Khadamat_FileService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Domain.FileAggregate
{
    public class KhadamatFile : AggregateRoot
    {
        public string OriginalFileName { get; private set; }
        public string StoredFileName { get; private set; }
        public string ContentType { get; private set; }
        public string Path { get; private set; }
        public DateTime UploadDate { get; private set; }
        public long SizeInBytes { get; set; }
        public string ETag { get; private set; }
        public KhadamatFile(string originalFileName, string contentType, long sizeInBytes, int id = 0) : base(id)
        {
            OriginalFileName = originalFileName;
            ContentType = contentType;
            SizeInBytes = sizeInBytes;
        }
        public void UpdateFileMetadata(string path, string storedFieName, IEntityTagGenerator eTagGenerator)
        {
            Path = path;
            StoredFileName = storedFieName;
            UploadDate = DateTime.Now;
            ETag = eTagGenerator.GenerateETag(this);
        }
        private KhadamatFile()
        {

        }
    }
}
