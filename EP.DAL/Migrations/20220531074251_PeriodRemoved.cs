using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EP.DAL.Migrations
{
    public partial class PeriodRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Composers_Periods_PeriodID",
                table: "Composers");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropIndex(
                name: "IX_Composers_PeriodID",
                table: "Composers");

            migrationBuilder.DropColumn(
                name: "PeriodID",
                table: "Composers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodID",
                table: "Composers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeginYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NamePeriode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Composers_PeriodID",
                table: "Composers",
                column: "PeriodID");

            migrationBuilder.AddForeignKey(
                name: "FK_Composers_Periods_PeriodID",
                table: "Composers",
                column: "PeriodID",
                principalTable: "Periods",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
