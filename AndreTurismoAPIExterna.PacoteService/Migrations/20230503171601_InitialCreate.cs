using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AndreTurismoAPIExterna.PacoteService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Pacote",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hotel = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Passagem = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Cliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacote", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pacote",
                schema: "dbo");
        }
    }
}
