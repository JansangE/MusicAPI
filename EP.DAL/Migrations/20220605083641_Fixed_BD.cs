using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EP.DAL.Migrations
{
    public partial class Fixed_BD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birtday",
                table: "Composers");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Composers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Composers");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birtday",
                table: "Composers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
