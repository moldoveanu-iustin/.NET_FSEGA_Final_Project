using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIPC_Web.Migrations
{
    /// <inheritdoc />
    public partial class V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rezervari",
                columns: table => new
                {
                    ID_Firma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    DataRezervare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataStartRezervare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataStopRezervare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocatiePlecare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocatieSosire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistantaTotala = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezervari_ClientId",
                table: "Rezervari",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
