using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Khadamat_FileService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFilesContextMetadataPathProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "FilesContextMetadata",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "FilesContextMetadata");
        }
    }
}
