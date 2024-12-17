using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIPC_Web.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRezervareV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rezervari");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rezervari",
                columns: table => new
                {
                    ID_Firma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    SoferId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataCursa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRezervare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocatiePlecare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocatieSosire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PretTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervari", x => x.ID_Firma);
                    table.ForeignKey(
                        name: "FK_Rezervari_Clienti_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervari_Soferi_SoferId1",
                        column: x => x.SoferId1,
                        principalTable: "Soferi",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezervari_ClientId",
                table: "Rezervari",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervari_SoferId1",
                table: "Rezervari",
                column: "SoferId1");
        }
    }
}
