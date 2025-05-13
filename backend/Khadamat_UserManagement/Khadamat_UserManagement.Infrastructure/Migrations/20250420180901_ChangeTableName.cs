using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Khadamat_UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterApplication_Users_HandledByUserId",
                table: "RegisterApplication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisterApplication",
                table: "RegisterApplication");

            migrationBuilder.RenameTable(
                name: "RegisterApplication",
                newName: "RegisterApplications");

            migrationBuilder.RenameIndex(
                name: "IX_RegisterApplication_Username",
                table: "RegisterApplications",
                newName: "IX_RegisterApplications_Username");

            migrationBuilder.RenameIndex(
                name: "IX_RegisterApplication_HandledByUserId",
                table: "RegisterApplications",
                newName: "IX_RegisterApplications_HandledByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RegisterApplication_Email",
                table: "RegisterApplications",
                newName: "IX_RegisterApplications_Email");

            migrationBuilder.AddColumn<int>(
                name: "Roles",
                table: "RegisterApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisterApplications",
                table: "RegisterApplications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterApplications_Users_HandledByUserId",
                table: "RegisterApplications",
                column: "HandledByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterApplications_Users_HandledByUserId",
                table: "RegisterApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisterApplications",
                table: "RegisterApplications");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "RegisterApplications");

            migrationBuilder.RenameTable(
                name: "RegisterApplications",
                newName: "RegisterApplication");

            migrationBuilder.RenameIndex(
                name: "IX_RegisterApplications_Username",
                table: "RegisterApplication",
                newName: "IX_RegisterApplication_Username");

            migrationBuilder.RenameIndex(
                name: "IX_RegisterApplications_HandledByUserId",
                table: "RegisterApplication",
                newName: "IX_RegisterApplication_HandledByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RegisterApplications_Email",
                table: "RegisterApplication",
                newName: "IX_RegisterApplication_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisterApplication",
                table: "RegisterApplication",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterApplication_Users_HandledByUserId",
                table: "RegisterApplication",
                column: "HandledByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
