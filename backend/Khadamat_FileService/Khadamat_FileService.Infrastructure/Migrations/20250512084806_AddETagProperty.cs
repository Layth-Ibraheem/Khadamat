﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Khadamat_FileService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddETagProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ETag",
                table: "KhadamatFiles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ETag",
                table: "KhadamatFiles");
        }
    }
}
