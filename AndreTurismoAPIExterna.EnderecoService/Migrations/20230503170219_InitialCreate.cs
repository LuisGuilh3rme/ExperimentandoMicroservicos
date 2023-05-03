using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AndreTurismoAPIExterna.EnderecoService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Cidade",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(50)", nullable: false, defaultValue: ""),
                    Numero = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Bairro = table.Column<string>(type: "varchar(50)", nullable: false, defaultValue: ""),
                    CEP = table.Column<string>(type: "char(8)", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(50)", nullable: false, defaultValue: ""),
                    CidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalSchema: "dbo",
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_CidadeId",
                schema: "dbo",
                table: "Endereco",
                column: "CidadeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Cidade",
                schema: "dbo");
        }
    }
}
