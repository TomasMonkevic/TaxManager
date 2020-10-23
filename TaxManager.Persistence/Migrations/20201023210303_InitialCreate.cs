using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxManager.Persistence.Migrations
{
    public partial class InitialCreate : Migration
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
                    To = table.Column<DateTime>(nullable: true),
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

            migrationBuilder.InsertData(
                table: "Manucipalities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Copenhagen" });

            migrationBuilder.InsertData(
                table: "Manucipalities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Vilnius" });

            migrationBuilder.InsertData(
                table: "Taxes",
                columns: new[] { "Id", "From", "MunicipalityId", "Rate", "TaxType", "To" },
                values: new object[] { 1, new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0.40000000000000002, 0, null });

            migrationBuilder.InsertData(
                table: "Taxes",
                columns: new[] { "Id", "From", "MunicipalityId", "Rate", "TaxType", "To" },
                values: new object[] { 2, new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0.20000000000000001, 3, new DateTime(2016, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Taxes",
                columns: new[] { "Id", "From", "MunicipalityId", "Rate", "TaxType", "To" },
                values: new object[] { 3, new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 0.20000000000000001, 3, new DateTime(2016, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });

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
