using System;
using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
#pragma warning disable CA1814 
namespace GsDotNet.Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GsDotNet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tipo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    DataOcorrencia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Local = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Severidade = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GsDotNet", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "PessoasAfetadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Endereco = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TipoAfetacao = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EventoClimaticoId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoasAfetadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoasAfetadas_GsDotNet_EventoClimaticoId",
                        column: x => x.EventoClimaticoId,
                        principalTable: "GsDotNet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.InsertData(
                table: "GsDotNet",
                columns: new[] { "Id", "Ativo", "DataCadastro", "DataOcorrencia", "Descricao", "Local", "Severidade", "Tipo" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 5, 30, 17, 25, 49, 979, DateTimeKind.Local).AddTicks(2519), new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enchente causada por chuvas intensas", "São Paulo, SP", 4, "Enchente" },
                    { 2, true, new DateTime(2025, 5, 30, 17, 25, 49, 979, DateTimeKind.Local).AddTicks(2530), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Período prolongado de estiagem", "Fortaleza, CE", 3, "Seca" }
                });
            migrationBuilder.InsertData(
                table: "PessoasAfetadas",
                columns: new[] { "Id", "Ativo", "CPF", "DataCadastro", "DataNascimento", "Email", "Endereco", "EventoClimaticoId", "Nome", "Telefone", "TipoAfetacao" },
                values: new object[,]
                {
                    { 1, true, "12345678901", new DateTime(2025, 5, 30, 17, 25, 49, 979, DateTimeKind.Local).AddTicks(2701), new DateTime(1985, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "joao.silva@email.com", "Rua das Flores, 123 - São Paulo, SP", 1, "João Silva", "(11) 98765-4321", "Desabrigado" },
                    { 2, true, "98765432101", new DateTime(2025, 5, 30, 17, 25, 49, 979, DateTimeKind.Local).AddTicks(2710), new DateTime(1990, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "maria.oliveira@email.com", "Av. Central, 456 - Fortaleza, CE", 2, "Maria Oliveira", "(85) 91234-5678", "Perda de plantação" }
                });
            migrationBuilder.CreateIndex(
                name: "IX_PessoasAfetadas_CPF",
                table: "PessoasAfetadas",
                column: "CPF",
                unique: true);
            migrationBuilder.CreateIndex(
                name: "IX_PessoasAfetadas_EventoClimaticoId",
                table: "PessoasAfetadas",
                column: "EventoClimaticoId");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PessoasAfetadas");
            migrationBuilder.DropTable(
                name: "GsDotNet");
        }
    }
}
