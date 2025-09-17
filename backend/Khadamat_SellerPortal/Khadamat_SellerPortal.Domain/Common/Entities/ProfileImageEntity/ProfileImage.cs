using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.Common.Entities.ProfileImageEntity
{
    public class ProfileImage : Entity
    {
        public string CachedImageFilePath { get; private set; }
        public int ImageFileId { get; private set; }
        public int SellerId { get; private set; }
        private ProfileImage(int imageFileId, string cachedImageFilePath, int sellerId, int id = 0) : base(id)
        {
            CachedImageFilePath = cachedImageFilePath;
            ImageFileId = imageFileId;
            SellerId = sellerId;
        }

        private ProfileImage()
        {

        }

        public static ErrorOr<ProfileImage> Create(int imageFileId, string cachedImageFilePath, int sellerId)
        {
            // enforce business rules if any

            return new ProfileImage(imageFileId, cachedImageFilePath, sellerId);
        }

        public ErrorOr<Success> Update(int imageFileId,string cachedImageFilePath)
        {
            // enforce business rules if any
            CachedImageFilePath = cachedImageFilePath;
            ImageFileId = imageFileId;
            return Result.Success;
        }
    }
}
