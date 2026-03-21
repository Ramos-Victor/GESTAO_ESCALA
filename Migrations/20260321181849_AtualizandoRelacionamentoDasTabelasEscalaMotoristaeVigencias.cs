using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gestao_Escala.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoRelacionamentoDasTabelasEscalaMotoristaeVigencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escala_Motorista_MotoristaId",
                table: "Escala");

            migrationBuilder.DropIndex(
                name: "IX_Escala_MotoristaId",
                table: "Escala");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Escala");

            migrationBuilder.DropColumn(
                name: "MotoristaId",
                table: "Escala");

            migrationBuilder.CreateTable(
                name: "VigenciaEscala",
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
                    table.PrimaryKey("PK_VigenciaEscala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VigenciaEscala_Escala_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "Escala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VigenciaMotorista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    VigenciaEscalaId = table.Column<int>(type: "integer", nullable: false),
                    MotoristaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VigenciaMotorista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VigenciaMotorista_Motorista_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "Motorista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VigenciaMotorista_VigenciaEscala_VigenciaEscalaId",
                        column: x => x.VigenciaEscalaId,
                        principalTable: "VigenciaEscala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VigenciaEscala_EscalaId",
                table: "VigenciaEscala",
                column: "EscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_VigenciaMotorista_MotoristaId",
                table: "VigenciaMotorista",
                column: "MotoristaId");

            migrationBuilder.CreateIndex(
                name: "IX_VigenciaMotorista_VigenciaEscalaId",
                table: "VigenciaMotorista",
                column: "VigenciaEscalaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VigenciaMotorista");

            migrationBuilder.DropTable(
                name: "VigenciaEscala");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Data",
                table: "Escala",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "MotoristaId",
                table: "Escala",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Escala_MotoristaId",
                table: "Escala",
                column: "MotoristaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Escala_Motorista_MotoristaId",
                table: "Escala",
                column: "MotoristaId",
                principalTable: "Motorista",
                principalColumn: "Id");
        }
    }
}
