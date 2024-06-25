using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almacen.Migrations
{
    /// <inheritdoc />
    public partial class trasladosedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreadoInmuebleId",
                table: "Traslados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Traslados_CreadoInmuebleId",
                table: "Traslados",
                column: "CreadoInmuebleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Traslados_Inmuebles_CreadoInmuebleId",
                table: "Traslados",
                column: "CreadoInmuebleId",
                principalTable: "Inmuebles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Traslados_Inmuebles_CreadoInmuebleId",
                table: "Traslados");

            migrationBuilder.DropIndex(
                name: "IX_Traslados_CreadoInmuebleId",
                table: "Traslados");

            migrationBuilder.DropColumn(
                name: "CreadoInmuebleId",
                table: "Traslados");
        }
    }
}
