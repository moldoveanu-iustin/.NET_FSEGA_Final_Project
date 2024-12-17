using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIPC_Web.Migrations
{
    /// <inheritdoc />
    public partial class V10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_Firma",
                table: "Rezervare",
                newName: "ID_Rezervare");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_Rezervare",
                table: "Rezervare",
                newName: "ID_Firma");
        }
    }
}
