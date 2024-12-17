using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIPC_Web.Migrations
{
    /// <inheritdoc />
    public partial class V19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rezervare",
                columns: table => new
                {
                    ID_Firma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataRezervare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataStartCursa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataStopCursa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocatiePlecare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocatieSosire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistantaTotala = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PretTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoferId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervare", x => x.ID_Firma);
                    table.ForeignKey(
                        name: "FK_Rezervare_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
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
                name: "IX_Rezervare_ClientId",
                table: "Rezervare",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervare_SoferId",
                table: "Rezervare",
                column: "SoferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rezervare");
        }
    }
}
