using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoTarefas.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lista",
                columns: table => new
                {
                    IdLista = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeLista = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    NomeAutor = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lista", x => x.IdLista);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "ListaTarefas",
                columns: table => new
                {
                    IdListaTarefas = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdLista = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaTarefas", x => x.IdListaTarefas);
                    table.ForeignKey(
                        name: "FK_ListaTarefas_Lista_IdLista",
                        column: x => x.IdLista,
                        principalTable: "Lista",
                        principalColumn: "IdLista",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "IdUsuario", "Ativo", "Email", "Nome", "Senha", "Token" },
                values: new object[] { new Guid("4ab52682-7f30-4f2a-abfc-313261d73761"), true, "administrador@gmail.com", "Administrador", "AAAAAAAAAAAAAAAAAAAAAA==.3wu0cnkZF7So7aGdZmThMQ==.B+gebXkAUyeUz6gi5e7LGWRG19Lyah1MFrg9sKxAoxw=", "" });

            migrationBuilder.CreateIndex(
                name: "IX_Lista_NomeLista",
                table: "Lista",
                column: "NomeLista",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListaTarefas_IdLista",
                table: "ListaTarefas",
                column: "IdLista");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email_Senha",
                table: "Usuario",
                columns: new[] { "Email", "Senha" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListaTarefas");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Lista");
        }
    }
}
