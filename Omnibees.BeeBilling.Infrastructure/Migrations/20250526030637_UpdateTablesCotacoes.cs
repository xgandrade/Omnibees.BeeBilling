using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Omnibees.BeeBilling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesCotacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdadeMaxima",
                table: "FaixasIdade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdadeMinima",
                table: "FaixasIdade",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdadeMaxima",
                table: "FaixasIdade");

            migrationBuilder.DropColumn(
                name: "IdadeMinima",
                table: "FaixasIdade");
        }
    }
}
