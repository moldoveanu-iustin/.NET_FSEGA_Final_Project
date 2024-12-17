using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIPC_Web.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Masini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnFabricatie = table.Column<int>(type: "int", nullable: false),
                    NumarInmatriculare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PretPerKm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FirmaId = table.Column<int>(type: "int", nullable: false),
                    imagine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imagine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreluataDeSofer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Masini_Firma_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firma",
                        principalColumn: "ID_Firma",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Soferi",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soferi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Soferi_Masini_MasinaId",
                        column: x => x.MasinaId,
                        principalTable: "Masini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervari",
                columns: table => new
                {
                    ID_Firma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataRezervare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCursa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocatiePlecare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocatieSosire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PretTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    SoferId = table.Column<int>(type: "int", nullable: false),
                    SoferId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                name: "IX_Masini_FirmaId",
                table: "Masini",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervari_ClientId",
                table: "Rezervari",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervari_SoferId1",
                table: "Rezervari",
                column: "SoferId1");

            migrationBuilder.CreateIndex(
                name: "IX_Soferi_MasinaId",
                table: "Soferi",
                column: "MasinaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Rezervari");

            migrationBuilder.DropTable(
                name: "Clienti");

            migrationBuilder.DropTable(
                name: "Soferi");

            migrationBuilder.DropTable(
                name: "Masini");

        }
    }
}
