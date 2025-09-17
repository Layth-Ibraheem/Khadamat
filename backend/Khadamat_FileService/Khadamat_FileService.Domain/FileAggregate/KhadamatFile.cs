using Khadamat_FileService.Domain.Common;
using Khadamat_FileService.Domain.Common.Interfaces;
using Khadamat_FileService.Domain.FileAggregate.Events;
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
        public KhadamatFileReferenceType KhadamatFileType { get; set; }
        public KhadamatFile(string originalFileName, string contentType, long sizeInBytes, KhadamatFileReferenceType khadamatFileType, int id = 0) : base(id)
        {
            OriginalFileName = originalFileName;
            ContentType = contentType;
            SizeInBytes = sizeInBytes;
            KhadamatFileType = khadamatFileType;
            
        }
        public void UpdateFileMetadata(string path, string storedFieName, IEntityTagGenerator eTagGenerator)
        {
            Path = path;
            StoredFileName = storedFieName;
            UploadDate = DateTime.Now;
            ETag = eTagGenerator.GenerateETag(this);
            if (KhadamatFileType == KhadamatFileReferenceType.SellerEducationCertificate)
            {
                _domainEvents.Add(new EducationFileSavedDomainEvent(Path, () => this.Id));
            }
            if (KhadamatFileType == KhadamatFileReferenceType.SellerWorkExperienceCertificate)
            {
                _domainEvents.Add(new WorkExperienceFileSavedDomainEvent(Path, () => Id));
            }
            if(KhadamatFileType == KhadamatFileReferenceType.SellerProfileImage)
            {
                _domainEvents.Add(new ProfileImageFileSavedDomainEvent(Path, () => Id));
            }



            // I will let this commented code in order to remember my self of how I overcome a domain problem
            // this problem solved by creating a table called file context and I am storing the context of the file (instituion, field of study, etc...)
            // and by this I can get the context later and publish an integration event

            //var nationalNo = ""; //path.Split(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar)[1];

            //// Violates DDD, needs emergency solution
            //if (metadata.TryGetValue("NationalNo", out nationalNo))
            //{
            //    if (metadata.TryGetValue("Institution", out var institution) && metadata.TryGetValue("FieldOfStudy", out var fieldOdStudy))
            //    {
            //        _domainEvents.Add(new EducationFileSaved(path, nationalNo, institution, fieldOdStudy));
            //        return;
            //    }
            //    if (metadata.TryGetValue("CompanyName", out var work))
            //    {

            //    }
            //}
        }
        private KhadamatFile()
        {

        }
    }
}
