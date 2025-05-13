using Khadamat_SellerPortal.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.PortfolioUrls.Persistence
{
    public class PortfolioUrlConfigurations : IEntityTypeConfiguration<PortfolioUrl>
    {
        public void Configure(EntityTypeBuilder<PortfolioUrl> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Url)
                .HasMaxLength(500)
                .IsRequired();

            // SmartEnum conversion
            builder.Property(x => x.Type)
                .HasConversion(
                    v => v.Value,
                    v => PortfolioUrlType.FromValue(v))
                .IsRequired();

            builder.ToTable("PortfolioUrls");
        }
    }
}
