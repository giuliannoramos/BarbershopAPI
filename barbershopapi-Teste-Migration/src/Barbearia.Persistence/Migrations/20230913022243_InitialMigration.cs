using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Barbearia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Telephone",
                columns: table => new
                {
                    TelephoneId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telephone", x => x.TelephoneId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: true),
                    TelephoneId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customer_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK_Customer_Telephone_TelephoneId",
                        column: x => x.TelephoneId,
                        principalTable: "Telephone",
                        principalColumn: "TelephoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "Cep", "City", "Complement", "CustomerId", "District", "Number", "State", "Street" },
                values: new object[,]
                {
                    { 1, "88888888", "Bc", "Perto de la", 1, "Teste", 100, "SC", "Rua logo ali" },
                    { 2, "88888888", "Itajaí", "Longe de la", 2, "Perto", 300, "SC", "Rua longe" }
                });

            migrationBuilder.InsertData(
                table: "Telephone",
                columns: new[] { "TelephoneId", "CustomerId", "Number", "Type" },
                values: new object[,]
                {
                    { 1, 1, "47988887777", 1 },
                    { 2, 2, "47988887777", 2 }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "AddressId", "BirthDate", "Cpf", "Email", "Gender", "Name", "TelephoneId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1999, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "73473943096", "veio@hotmail.com", 1, "Linus Torvalds", 1 },
                    { 2, 2, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "73473003096", "bill@gmail.com", 2, "Bill Gates", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AddressId",
                table: "Customer",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_TelephoneId",
                table: "Customer",
                column: "TelephoneId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Telephone");
        }
    }
}
