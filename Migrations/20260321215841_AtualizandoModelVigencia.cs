using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gestao_Escala.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoModelVigencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VigenciaMotorista_VigenciaEscala_VigenciaEscalaId",
                table: "VigenciaMotorista");

            migrationBuilder.DropTable(
                name: "VigenciaEscala");

            migrationBuilder.RenameColumn(
                name: "VigenciaEscalaId",
                table: "VigenciaMotorista",
                newName: "VigenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_VigenciaMotorista_VigenciaEscalaId",
                table: "VigenciaMotorista",
                newName: "IX_VigenciaMotorista_VigenciaId");

            migrationBuilder.CreateTable(
                name: "Vigencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    EscalaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vigencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vigencia_Escala_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "Escala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vigencia_EscalaId",
                table: "Vigencia",
                column: "EscalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VigenciaMotorista_Vigencia_VigenciaId",
                table: "VigenciaMotorista",
                column: "VigenciaId",
                principalTable: "Vigencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VigenciaMotorista_Vigencia_VigenciaId",
                table: "VigenciaMotorista");

            migrationBuilder.DropTable(
                name: "Vigencia");

            migrationBuilder.RenameColumn(
                name: "VigenciaId",
                table: "VigenciaMotorista",
                newName: "VigenciaEscalaId");

            migrationBuilder.RenameIndex(
                name: "IX_VigenciaMotorista_VigenciaId",
                table: "VigenciaMotorista",
                newName: "IX_VigenciaMotorista_VigenciaEscalaId");

            migrationBuilder.CreateTable(
                name: "VigenciaEscala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EscalaId = table.Column<int>(type: "integer", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: false),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VigenciaEscala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VigenciaEscala_Escala_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "Escala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VigenciaEscala_EscalaId",
                table: "VigenciaEscala",
                column: "EscalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VigenciaMotorista_VigenciaEscala_VigenciaEscalaId",
                table: "VigenciaMotorista",
                column: "VigenciaEscalaId",
                principalTable: "VigenciaEscala",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
