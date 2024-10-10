using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessCardApi.Migrations
{
    /// <inheritdoc />
    public partial class ThirdCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "photo",
                table: "BusinessCard",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "BusinessCard",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "BusinessCard",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "BusinessCard",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "BusinessCard",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "dob",
                table: "BusinessCard",
                newName: "DOB");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "BusinessCard",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "BusinessCard",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "BusinessCard",
                newName: "photo");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "BusinessCard",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BusinessCard",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "BusinessCard",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "BusinessCard",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "DOB",
                table: "BusinessCard",
                newName: "dob");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "BusinessCard",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BusinessCard",
                newName: "id");
        }
    }
}
