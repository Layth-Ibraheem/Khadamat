using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Khadamat_FileService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddKhadamatFileReferenceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KhadamatFileType",
                table: "KhadamatFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KhadamatFileType",
                table: "KhadamatFiles");
        }
    }
}
