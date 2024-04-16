using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelPredictor.Migrations
{
    public partial class UpdateCarburant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrixJournalier_Station_StationId",
                schema: "Identity",
                table: "PrixJournalier");

            migrationBuilder.RenameColumn(
                name: "StationId",
                schema: "Identity",
                table: "PrixJournalier",
                newName: "IDCarburant");

            migrationBuilder.RenameIndex(
                name: "IX_PrixJournalier_StationId",
                schema: "Identity",
                table: "PrixJournalier",
                newName: "IX_PrixJournalier_IDCarburant");

            migrationBuilder.AddColumn<int>(
                name: "IDStation",
                schema: "Identity",
                table: "PrixJournalier",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PrixJournalier_IDStation",
                schema: "Identity",
                table: "PrixJournalier",
                column: "IDStation");

            migrationBuilder.AddForeignKey(
                name: "FK_PrixJournalier_Carburant_IDCarburant",
                schema: "Identity",
                table: "PrixJournalier",
                column: "IDCarburant",
                principalSchema: "Identity",
                principalTable: "Carburant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrixJournalier_Station_IDStation",
                schema: "Identity",
                table: "PrixJournalier",
                column: "IDStation",
                principalSchema: "Identity",
                principalTable: "Station",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrixJournalier_Carburant_IDCarburant",
                schema: "Identity",
                table: "PrixJournalier");

            migrationBuilder.DropForeignKey(
                name: "FK_PrixJournalier_Station_IDStation",
                schema: "Identity",
                table: "PrixJournalier");

            migrationBuilder.DropIndex(
                name: "IX_PrixJournalier_IDStation",
                schema: "Identity",
                table: "PrixJournalier");

            migrationBuilder.DropColumn(
                name: "IDStation",
                schema: "Identity",
                table: "PrixJournalier");

            migrationBuilder.RenameColumn(
                name: "IDCarburant",
                schema: "Identity",
                table: "PrixJournalier",
                newName: "StationId");

            migrationBuilder.RenameIndex(
                name: "IX_PrixJournalier_IDCarburant",
                schema: "Identity",
                table: "PrixJournalier",
                newName: "IX_PrixJournalier_StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrixJournalier_Station_StationId",
                schema: "Identity",
                table: "PrixJournalier",
                column: "StationId",
                principalSchema: "Identity",
                principalTable: "Station",
                principalColumn: "Id");
        }
    }
}
