using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelPredictor.Migrations
{
    public partial class Ville : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IDVille",
                schema: "Identity",
                table: "Station",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ville",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ville", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Station_IDVille",
                schema: "Identity",
                table: "Station",
                column: "IDVille");

            migrationBuilder.AddForeignKey(
                name: "FK_Station_Ville_IDVille",
                schema: "Identity",
                table: "Station",
                column: "IDVille",
                principalSchema: "Identity",
                principalTable: "Ville",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Station_Ville_IDVille",
                schema: "Identity",
                table: "Station");

            migrationBuilder.DropTable(
                name: "Ville",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Station_IDVille",
                schema: "Identity",
                table: "Station");

            migrationBuilder.DropColumn(
                name: "IDVille",
                schema: "Identity",
                table: "Station");
        }
    }
}
