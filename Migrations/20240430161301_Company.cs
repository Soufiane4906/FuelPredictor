using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelPredictor.Migrations
{
    public partial class Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IDCompany",
                schema: "Identity",
                table: "Station",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "prix",
                schema: "Identity",
                table: "PrixJournalier",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Station_IDCompany",
                schema: "Identity",
                table: "Station",
                column: "IDCompany");

            migrationBuilder.AddForeignKey(
                name: "FK_Station_Companies_IDCompany",
                schema: "Identity",
                table: "Station",
                column: "IDCompany",
                principalSchema: "Identity",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Station_Companies_IDCompany",
                schema: "Identity",
                table: "Station");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Station_IDCompany",
                schema: "Identity",
                table: "Station");

            migrationBuilder.DropColumn(
                name: "IDCompany",
                schema: "Identity",
                table: "Station");

            migrationBuilder.DropColumn(
                name: "prix",
                schema: "Identity",
                table: "PrixJournalier");
        }
    }
}
