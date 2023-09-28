using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Barbearia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Correcao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    PersonType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Street = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    District = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    City = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    State = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Complement = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telephone",
                columns: table => new
                {
                    TelephoneId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telephone", x => x.TelephoneId);
                    table.ForeignKey(
                        name: "FK_Telephone_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "PersonId", "BirthDate", "Cnpj", "Cpf", "Email", "Gender", "Name", "PersonType" },
                values: new object[,]
                {
                    { 1, new DateOnly(1999, 8, 7), "", "73473943096", "veio@hotmail.com", 1, "Linus Torvalds", 2 },
                    { 2, new DateOnly(2000, 1, 1), "", "73473003096", "bill@gmail.com", 2, "Bill Gates", 2 },
                    { 3, new DateOnly(1973, 2, 1), "", "73473943096", "josefacraft@hotmail.com", 2, "Josefina", 3 },
                    { 4, new DateOnly(1975, 4, 4), "73473003096986", "", "micro@so.ft", 0, "Microsoft", 3 }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "Cep", "City", "Complement", "District", "Number", "PersonId", "State", "Street" },
                values: new object[,]
                {
                    { 1, "88888888", "Bc", "Perto de la", "Teste", 100, 1, "SC", "Rua logo ali" },
                    { 2, "88888888", "Itajaí", "Longe de la", "Perto", 300, 2, "SC", "Rua longe" },
                    { 3, "80888088", "Bc", "Perto", "Asilo", 100, 3, "SC", "Rua velha" },
                    { 4, "88123888", "Floripa", "Longe", "soft", 300, 4, "SC", "Rua micro" }
                });

            migrationBuilder.InsertData(
                table: "Telephone",
                columns: new[] { "TelephoneId", "Number", "PersonId", "Type" },
                values: new object[,]
                {
                    { 1, "47988887777", 1, 1 },
                    { 2, "47988887777", 2, 2 },
                    { 3, "47944887777", 3, 1 },
                    { 4, "55988844777", 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_PersonId",
                table: "Address",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Telephone_PersonId",
                table: "Telephone",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Telephone");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
