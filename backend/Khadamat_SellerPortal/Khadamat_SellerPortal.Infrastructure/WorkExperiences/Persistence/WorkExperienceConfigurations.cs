using Khadamat_SellerPortal.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.WorkExperiences.Persistence
{
    public class WorkExperienceConfigurations : IEntityTypeConfiguration<WorkExperience>
    {
        public void Configure(EntityTypeBuilder<WorkExperience> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CompanyName)
                .HasMaxLength(200)
                .IsRequired(true);

            builder.Property(x => x.Position)
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.Field)
                .HasMaxLength(50);

            builder.OwnsOne(x => x.Range, dr =>
            {
                dr.Property(x => x.Start).IsRequired(true).HasColumnName("StartDate");
                dr.Property(x => x.End).IsRequired(false).HasColumnName("EndDate");
                dr.Property(x => x.UntilNow).IsRequired(true).HasColumnName("UntilNow");
            });

            builder.HasMany(x => x.Certificates)
                .WithOne()
                .HasForeignKey(x => x.WorkExperienceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("WorkExperiences");
        }
    }
}
