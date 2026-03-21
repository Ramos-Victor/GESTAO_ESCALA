using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestao_Escala.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusEscalaeMotorista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Escala",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Escala");
        }
    }
}