using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Omnibees.BeeBilling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableCotacaoFKsAndPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cotacoes_Parceiros_ParceiroId",
                table: "Cotacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Cotacoes_Produtos_ProdutoId",
                table: "Cotacoes");

            migrationBuilder.DropIndex(
                name: "IX_Cotacoes_ParceiroId",
                table: "Cotacoes");

            migrationBuilder.DropIndex(
                name: "IX_Cotacoes_ProdutoId",
                table: "Cotacoes");

            migrationBuilder.DropColumn(
                name: "ParceiroId",
                table: "Cotacoes");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Cotacoes");

            migrationBuilder.AlterColumn<int>(
                name: "Telefone",
                table: "Cotacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DDD",
                table: "Cotacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Cotacoes_IdParceiro",
                table: "Cotacoes",
                column: "IdParceiro");

            migrationBuilder.CreateIndex(
                name: "IX_Cotacoes_IdProduto",
                table: "Cotacoes",
                column: "IdProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_Cotacoes_Parceiros_IdParceiro",
                table: "Cotacoes",
                column: "IdParceiro",
                principalTable: "Parceiros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cotacoes_Produtos_IdProduto",
                table: "Cotacoes",
                column: "IdProduto",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cotacoes_Parceiros_IdParceiro",
                table: "Cotacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Cotacoes_Produtos_IdProduto",
                table: "Cotacoes");

            migrationBuilder.DropIndex(
                name: "IX_Cotacoes_IdParceiro",
                table: "Cotacoes");

            migrationBuilder.DropIndex(
                name: "IX_Cotacoes_IdProduto",
                table: "Cotacoes");

            migrationBuilder.AlterColumn<int>(
                name: "Telefone",
                table: "Cotacoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DDD",
                table: "Cotacoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParceiroId",
                table: "Cotacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "Cotacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cotacoes_ParceiroId",
                table: "Cotacoes",
                column: "ParceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Cotacoes_ProdutoId",
                table: "Cotacoes",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cotacoes_Parceiros_ParceiroId",
                table: "Cotacoes",
                column: "ParceiroId",
                principalTable: "Parceiros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cotacoes_Produtos_ProdutoId",
                table: "Cotacoes",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
