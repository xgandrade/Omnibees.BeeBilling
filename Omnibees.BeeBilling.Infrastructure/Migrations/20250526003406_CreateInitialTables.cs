using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Omnibees.BeeBilling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitialTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coberturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coberturas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FaixasIdade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    Agravo = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaixasIdade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parceiros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parceiros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parentescos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parentescos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    Limite = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cotacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeSegurado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    IdParceiro = table.Column<int>(type: "int", nullable: false),
                    DDD = table.Column<int>(type: "int", nullable: false),
                    Telefone = table.Column<int>(type: "int", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Premio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImportanciaSegurada = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    ParceiroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cotacoes_Parceiros_ParceiroId",
                        column: x => x.ParceiroId,
                        principalTable: "Parceiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cotacoes_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CotacoesBeneficiario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percentual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdCotacao = table.Column<int>(type: "int", nullable: false),
                    IdParentesco = table.Column<int>(type: "int", nullable: false),
                    CotacaoId = table.Column<int>(type: "int", nullable: false),
                    ParentescoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotacoesBeneficiario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CotacoesBeneficiario_Cotacoes_CotacaoId",
                        column: x => x.CotacaoId,
                        principalTable: "Cotacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CotacoesBeneficiario_Parentescos_ParentescoId",
                        column: x => x.ParentescoId,
                        principalTable: "Parentescos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CotacoesCobertura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCotacao = table.Column<int>(type: "int", nullable: false),
                    IdCobertura = table.Column<int>(type: "int", nullable: false),
                    ValorDesconto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorAgravo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CotacaoId = table.Column<int>(type: "int", nullable: false),
                    CoberturaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotacoesCobertura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CotacoesCobertura_Coberturas_CoberturaId",
                        column: x => x.CoberturaId,
                        principalTable: "Coberturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CotacoesCobertura_Cotacoes_CotacaoId",
                        column: x => x.CotacaoId,
                        principalTable: "Cotacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cotacoes_ParceiroId",
                table: "Cotacoes",
                column: "ParceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Cotacoes_ProdutoId",
                table: "Cotacoes",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_CotacoesBeneficiario_CotacaoId",
                table: "CotacoesBeneficiario",
                column: "CotacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_CotacoesBeneficiario_ParentescoId",
                table: "CotacoesBeneficiario",
                column: "ParentescoId");

            migrationBuilder.CreateIndex(
                name: "IX_CotacoesCobertura_CoberturaId",
                table: "CotacoesCobertura",
                column: "CoberturaId");

            migrationBuilder.CreateIndex(
                name: "IX_CotacoesCobertura_CotacaoId",
                table: "CotacoesCobertura",
                column: "CotacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CotacoesBeneficiario");

            migrationBuilder.DropTable(
                name: "CotacoesCobertura");

            migrationBuilder.DropTable(
                name: "FaixasIdade");

            migrationBuilder.DropTable(
                name: "Parentescos");

            migrationBuilder.DropTable(
                name: "Coberturas");

            migrationBuilder.DropTable(
                name: "Cotacoes");

            migrationBuilder.DropTable(
                name: "Parceiros");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
