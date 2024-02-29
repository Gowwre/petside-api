using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHealthCare.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProvidersId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProvidersId",
                table: "Appointments",
                column: "ProvidersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Providers_ProvidersId",
                table: "Appointments",
                column: "ProvidersId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Providers_ProvidersId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ProvidersId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ProvidersId",
                table: "Appointments");
        }
    }
}
