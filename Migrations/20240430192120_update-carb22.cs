using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelPredictor.Migrations
{
    public partial class updatecarb22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeCarburant",
                schema: "Identity",
                table: "Carburant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeCarburant",
                schema: "Identity",
                table: "Carburant");
        }
    }
}
