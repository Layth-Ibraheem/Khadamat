using Khadamat_SellerPortal.Domain.Common.Entities.SocialMediaLinkEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.SocialMediaLinks.Persistence
{
    public class SocialMediaLinkConfigurations : IEntityTypeConfiguration<SocialMediaLink>
    {
        public void Configure(EntityTypeBuilder<SocialMediaLink> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Link)
                .HasMaxLength(500)
                .IsRequired();

            // SmartEnum conversion
            builder.Property(x => x.Type)
                .HasConversion(
                    v => v.Value,
                    v => SocialMediaLinkType.FromValue(v))
                .IsRequired();

            builder.ToTable("SocialMediaLinks");
        }
    }
}
