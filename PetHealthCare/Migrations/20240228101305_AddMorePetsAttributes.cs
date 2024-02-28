using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHealthCare.Migrations
{
    /// <inheritdoc />
    public partial class AddMorePetsAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Height",
                schema: "dbo",
                table: "Pets",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentifyingFeatures",
                schema: "dbo",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                schema: "dbo",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "IdentifyingFeatures",
                schema: "dbo",
                table: "Pets");
        }
    }
}
