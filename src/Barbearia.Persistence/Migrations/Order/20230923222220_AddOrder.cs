using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Barbearia.Persistence.Migrations.Order
{
    /// <inheritdoc />
    public partial class AddOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CouponId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CouponCode = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    DiscountPercent = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    BuyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BuyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GrossTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    NetTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    CouponId = table.Column<int>(type: "integer", nullable: true),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "CouponId");
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "CreationDate", "DiscountPercent", "ExpirationDate" },
                values: new object[] { 1, "teste3", new DateTime(2023, 9, 23, 22, 22, 19, 951, DateTimeKind.Utc).AddTicks(5385), 10, new DateTime(2023, 9, 23, 22, 22, 19, 951, DateTimeKind.Utc).AddTicks(5387) });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "BuyDate", "Number", "PersonId", "Status" },
                values: new object[] { 1, new DateTime(2023, 9, 23, 22, 22, 19, 951, DateTimeKind.Utc).AddTicks(4992), 500, 1, 2 });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "BuyDate", "CouponId", "Description", "GrossTotal", "NetTotal", "OrderId", "PaymentMethod", "Status" },
                values: new object[] { 1, new DateTime(2023, 9, 23, 22, 22, 19, 951, DateTimeKind.Utc).AddTicks(5368), null, "Para de ler isso aqui e vai programar", 80m, 60m, 1, "Dinheiro", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PersonId",
                table: "Orders",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CouponId",
                table: "Payments",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
