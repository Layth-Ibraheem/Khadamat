using Khadamat_SellerPortal.Domain.Common.Entities.ProfileImageEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.ProfileImages.Persistence
{
    public class ProfileImageConfigurations : IEntityTypeConfiguration<ProfileImage>
    {
        public void Configure(EntityTypeBuilder<ProfileImage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CachedImageFilePath)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(x => x.ImageFileId)
                .IsRequired(true);
        }
    }
}
