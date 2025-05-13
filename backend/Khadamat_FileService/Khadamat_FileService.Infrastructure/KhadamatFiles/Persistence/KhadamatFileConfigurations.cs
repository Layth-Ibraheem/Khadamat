using Khadamat_FileService.Domain.FileAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Infrastructure.KhadamatFiles.Persistence
{
    public class KhadamatFileConfigurations : IEntityTypeConfiguration<KhadamatFile>
    {
        public void Configure(EntityTypeBuilder<KhadamatFile> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .ValueGeneratedOnAdd();

            builder.Property(f => f.ContentType)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(f => f.ETag)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(f => f.SizeInBytes)
                .IsRequired();

            builder.Property(f => f.UploadDate)
                .IsRequired();

            builder.Property(f => f.StoredFileName)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(f => f.OriginalFileName)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(f => f.Path)
                .HasMaxLength(1500)
                .IsRequired();

        }
    }
}
