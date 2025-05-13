using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Khadamat_UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationBetweenUserHandledApplicationAndApplicationToBeOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RegisterApplications_HandledByUserId",
                table: "RegisterApplications");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterApplications_HandledByUserId",
                table: "RegisterApplications",
                column: "HandledByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RegisterApplications_HandledByUserId",
                table: "RegisterApplications");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterApplications_HandledByUserId",
                table: "RegisterApplications",
                column: "HandledByUserId",
                unique: true,
                filter: "[HandledByUserId] IS NOT NULL");
        }
    }
}
