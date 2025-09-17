using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Khadamat_FileService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceStringPorpsWithIdsInFileContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "FilesContextMetadata");

            migrationBuilder.DropColumn(
                name: "FieldOfStudy",
                table: "FilesContextMetadata");

            migrationBuilder.DropColumn(
                name: "Institution",
                table: "FilesContextMetadata");

            migrationBuilder.DropColumn(
                name: "NationalNo",
                table: "FilesContextMetadata");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "FilesContextMetadata");

            migrationBuilder.AddColumn<int>(
                name: "CertificateId",
                table: "FilesContextMetadata",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "FilesContextMetadata",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "FilesContextMetadata",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkExperienceId",
                table: "FilesContextMetadata",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "FilesContextMetadata");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "FilesContextMetadata");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "FilesContextMetadata");

            migrationBuilder.DropColumn(
                name: "WorkExperienceId",
                table: "FilesContextMetadata");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "FilesContextMetadata",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldOfStudy",
                table: "FilesContextMetadata",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Institution",
                table: "FilesContextMetadata",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalNo",
                table: "FilesContextMetadata",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "FilesContextMetadata",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
