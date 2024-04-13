using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelPredictor.Migrations
{
    public partial class AddedCustom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Station_User_GerantId",
                schema: "Identity",
                table: "Station");

            migrationBuilder.DropIndex(
                name: "IX_Station_GerantId",
                schema: "Identity",
                table: "Station");

            migrationBuilder.DropColumn(
                name: "GerantId",
                schema: "Identity",
                table: "Station");

            migrationBuilder.AddColumn<string>(
                name: "IDGerant",
                schema: "Identity",
                table: "Station",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Station_IDGerant",
                schema: "Identity",
                table: "Station",
                column: "IDGerant");

            migrationBuilder.AddForeignKey(
                name: "FK_Station_User_IDGerant",
                schema: "Identity",
                table: "Station",
                column: "IDGerant",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Station_User_IDGerant",
                schema: "Identity",
                table: "Station");

            migrationBuilder.DropIndex(
                name: "IX_Station_IDGerant",
                schema: "Identity",
                table: "Station");

            migrationBuilder.DropColumn(
                name: "IDGerant",
                schema: "Identity",
                table: "Station");

            migrationBuilder.AddColumn<string>(
                name: "GerantId",
                schema: "Identity",
                table: "Station",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Station_GerantId",
                schema: "Identity",
                table: "Station",
                column: "GerantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Station_User_GerantId",
                schema: "Identity",
                table: "Station",
                column: "GerantId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
