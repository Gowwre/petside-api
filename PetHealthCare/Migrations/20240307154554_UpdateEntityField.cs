using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHealthCare.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUpgrade",
                schema: "dbo",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpgradeDate",
                schema: "dbo",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Providers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Providers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpgrade",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpgradeDate",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Providers");
        }
    }
}
