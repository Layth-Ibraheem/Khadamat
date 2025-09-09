using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Khadamat_FileService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFilesContextMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilesContextMetadata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Institution = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FieldOfStudy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesContextMetadata", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesContextMetadata");
        }
    }
}
