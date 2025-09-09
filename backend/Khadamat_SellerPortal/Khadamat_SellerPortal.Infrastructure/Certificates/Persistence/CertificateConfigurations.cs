using Khadamat_SellerPortal.Domain.Common.Entities.CertificateEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.Certificates.Persistence
{
    public class CertificateConfigurations : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CachedFilePath)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(250);

            // Nullable foreign keys (could belong to either WorkExperience or Education)
            builder.Property(x => x.WorkExperienceId).IsRequired(false);
            builder.Property(x => x.EducationId).IsRequired(false);

            builder.ToTable("Certificates");
        }
    }
}
