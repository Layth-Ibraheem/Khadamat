using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Khadamat_SellerPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFilePathToCachedFilePathInCertificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Certificates",
                newName: "CachedFilePath");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Certificates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Certificates");

            migrationBuilder.RenameColumn(
                name: "CachedFilePath",
                table: "Certificates",
                newName: "FilePath");
        }
    }
}
