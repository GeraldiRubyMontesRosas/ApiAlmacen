using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almacen.Migrations
{
    /// <inheritdoc />
    public partial class inmueblescambios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Traslados_Inmuebles_InmuebleId",
                table: "Traslados");

            migrationBuilder.AlterColumn<int>(
                name: "InmuebleId",
                table: "Traslados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Factura",
                table: "Inmuebles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Traslados_Inmuebles_InmuebleId",
                table: "Traslados",
                column: "InmuebleId",
                principalTable: "Inmuebles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Traslados_Inmuebles_InmuebleId",
                table: "Traslados");

            migrationBuilder.DropColumn(
                name: "Factura",
                table: "Inmuebles");

            migrationBuilder.AlterColumn<int>(
                name: "InmuebleId",
                table: "Traslados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Traslados_Inmuebles_InmuebleId",
                table: "Traslados",
                column: "InmuebleId",
                principalTable: "Inmuebles",
                principalColumn: "Id");
        }
    }
}
