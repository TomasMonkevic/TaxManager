using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxManager.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manucipalities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manucipalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rate = table.Column<double>(nullable: false),
                    TaxType = table.Column<int>(nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    MunicipalityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Taxes_Manucipalities_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Manucipalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Taxes_MunicipalityId",
                table: "Taxes",
                column: "MunicipalityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "Manucipalities");
        }
    }
}
