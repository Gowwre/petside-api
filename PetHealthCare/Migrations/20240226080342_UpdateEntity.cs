using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHealthCare.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offerings_Providers_ProvidersId",
                table: "Offerings");

            migrationBuilder.DropIndex(
                name: "IX_Offerings_ProvidersId",
                table: "Offerings");

            migrationBuilder.DropColumn(
                name: "ProvidersId",
                table: "Offerings");

            migrationBuilder.AddColumn<long>(
                name: "OtpEmail",
                schema: "dbo",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitType",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OfferProviders",
                columns: table => new
                {
                    ProvidersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferProviders", x => new { x.OfferingsId, x.ProvidersId });
                    table.ForeignKey(
                        name: "FK_OfferProviders_Offerings_OfferingsId",
                        column: x => x.OfferingsId,
                        principalTable: "Offerings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferProviders_Providers_ProvidersId",
                        column: x => x.ProvidersId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferProviders_ProvidersId",
                table: "OfferProviders",
                column: "ProvidersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferProviders");

            migrationBuilder.DropColumn(
                name: "OtpEmail",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "VisitType",
                table: "Appointments");

            migrationBuilder.AddColumn<Guid>(
                name: "ProvidersId",
                table: "Offerings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Offerings_ProvidersId",
                table: "Offerings",
                column: "ProvidersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offerings_Providers_ProvidersId",
                table: "Offerings",
                column: "ProvidersId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
