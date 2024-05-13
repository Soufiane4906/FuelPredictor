using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelPredictor.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                schema: "Identity",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                schema: "Identity",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                schema: "Identity",
                table: "Companies");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                schema: "Identity",
                table: "Companies",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
