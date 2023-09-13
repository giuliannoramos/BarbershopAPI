using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbearia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TPH2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Telephone_PersonId",
                table: "Telephone");

            migrationBuilder.DropIndex(
                name: "IX_Address_PersonId",
                table: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_Telephone_PersonId",
                table: "Telephone",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_PersonId",
                table: "Address",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Telephone_PersonId",
                table: "Telephone");

            migrationBuilder.DropIndex(
                name: "IX_Address_PersonId",
                table: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_Telephone_PersonId",
                table: "Telephone",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_PersonId",
                table: "Address",
                column: "PersonId",
                unique: true);
        }
    }
}
