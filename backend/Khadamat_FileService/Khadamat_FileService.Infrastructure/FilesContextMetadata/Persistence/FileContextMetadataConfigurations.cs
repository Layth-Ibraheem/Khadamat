using Khadamat_FileService.Application.Common.FileContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Khadamat_FileService.Infrastructure.FilesContextMetadata.Persistence
{
    public class FileContextMetadataConfigurations : IEntityTypeConfiguration<FileContextMetadata>
    {
        public void Configure(EntityTypeBuilder<FileContextMetadata> builder)
        {
            builder
            .Property<int>("Id")
            .ValueGeneratedOnAdd();

            builder.HasKey("Id");
            builder.Property(c => c.FilePath).HasMaxLength(1000);
        }
    }
}
