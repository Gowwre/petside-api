using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHealthCare.Migrations
{
    /// <inheritdoc />
    public partial class NotificationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Offerings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Offerings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameMedicine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateRemind = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeRemind = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dusage = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PetsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Pets_PetsId",
                        column: x => x.PetsId,
                        principalSchema: "dbo",
                        principalTable: "Pets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UsersId",
                        column: x => x.UsersId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PetsId",
                table: "Notifications",
                column: "PetsId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UsersId",
                table: "Notifications",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Offerings");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Offerings");
        }
    }
}
