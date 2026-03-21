using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestao_Escala.Migrations
{
    /// <inheritdoc />
    public partial class AlterEscalaMotoristaOpcional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escala_Motorista_MotoristaId",
                table: "Escala");

            migrationBuilder.AlterColumn<int>(
                name: "MotoristaId",
                table: "Escala",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Escala_Motorista_MotoristaId",
                table: "Escala",
                column: "MotoristaId",
                principalTable: "Motorista",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escala_Motorista_MotoristaId",
                table: "Escala");

            migrationBuilder.AlterColumn<int>(
                name: "MotoristaId",
                table: "Escala",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Escala_Motorista_MotoristaId",
                table: "Escala",
                column: "MotoristaId",
                principalTable: "Motorista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}