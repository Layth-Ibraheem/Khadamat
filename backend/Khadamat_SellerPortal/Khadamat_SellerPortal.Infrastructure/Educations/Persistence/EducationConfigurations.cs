using Khadamat_SellerPortal.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Infrastructure.Educations.Persistence
{
    public class EducationConfigurations : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Institution)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.FieldOfStudy)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Degree)
                .HasConversion(
                    v => v.Value,
                    v => EducationDegree.FromValue(v))
                .IsRequired();

            builder.Property(x => x.IsGraduated)
                .IsRequired();

            builder.OwnsOne(x => x.AttendancePeriod, ap =>
            {
                ap.Property(x => x.Start).IsRequired().HasColumnName("Start");
                ap.Property(x => x.End).HasColumnName("End");
                ap.Ignore(r => r.UntilNow);
            });

            builder.HasOne(x => x.EducationCertificate)
                .WithOne()
                .HasForeignKey<Certificate>(x => x.EducationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Educations");
        }
    }
}
