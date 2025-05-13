using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Domain.FileAggregate
{
    public class KhadamatFileReferenceType : SmartEnum<KhadamatFileReferenceType>
    {
        public static readonly KhadamatFileReferenceType SellerEducationCertificate = new KhadamatFileReferenceType("SellerEducationCertificate", 0);
        public static readonly KhadamatFileReferenceType SellerWorkExperienceCertificate = new KhadamatFileReferenceType("SellerWorkExperienceCertificate", 1);
        public static readonly KhadamatFileReferenceType SellerProfileImage = new KhadamatFileReferenceType("SellerProfileImage", 2);
        public static readonly KhadamatFileReferenceType ChattingFile = new KhadamatFileReferenceType("ChattingFile", 2);
        public KhadamatFileReferenceType(string name, int value) : base(name, value)
        {
        }
    }
}
