using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessCardApi.Migrations
{
    /// <inheritdoc />
    public partial class SeconCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessCards",
                table: "BusinessCards");

            migrationBuilder.RenameTable(
                name: "BusinessCards",
                newName: "BusinessCard");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessCard",
                table: "BusinessCard",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessCard",
                table: "BusinessCard");

            migrationBuilder.RenameTable(
                name: "BusinessCard",
                newName: "BusinessCards");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessCards",
                table: "BusinessCards",
                column: "id");
        }
    }
}
