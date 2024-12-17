using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIPC_Web.Migrations
{
    /// <inheritdoc />
    public partial class V7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rezervare");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rezervare",
                columns: table => new
                {
                    ID_Firma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId1 = table.Column<int>(type: "int", nullable: false),
                    SoferId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataRezervare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataStartCursa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataStopCursa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DistantaTotala = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LocatiePlecare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocatieSosire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PretTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervare", x => x.ID_Firma);
                    table.ForeignKey(
                        name: "FK_Rezervare_Clienti_ClientId1",
                        column: x => x.ClientId1,
                        principalTable: "Clienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervare_Soferi_SoferId",
                        column: x => x.SoferId,
                        principalTable: "Soferi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezervare_ClientId1",
                table: "Rezervare",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervare_SoferId",
                table: "Rezervare",
                column: "SoferId");
        }
    }
}
