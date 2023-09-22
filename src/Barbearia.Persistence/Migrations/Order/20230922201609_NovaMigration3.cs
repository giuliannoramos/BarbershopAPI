using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbearia.Persistence.Migrations.Order
{
    /// <inheritdoc />
    public partial class NovaMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "CreationDate", "ExpirationDate" },
                values: new object[] { new DateTime(2023, 9, 22, 20, 16, 9, 923, DateTimeKind.Utc).AddTicks(9416), new DateTime(2023, 9, 22, 20, 16, 9, 923, DateTimeKind.Utc).AddTicks(9416) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 9, 22, 20, 16, 9, 923, DateTimeKind.Utc).AddTicks(9312));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 9, 22, 20, 16, 9, 923, DateTimeKind.Utc).AddTicks(9404));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "CreationDate", "ExpirationDate" },
                values: new object[] { new DateTime(2023, 9, 22, 20, 14, 28, 7, DateTimeKind.Utc).AddTicks(214), new DateTime(2023, 9, 22, 20, 14, 28, 7, DateTimeKind.Utc).AddTicks(215) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 9, 22, 20, 14, 28, 7, DateTimeKind.Utc).AddTicks(111));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 9, 22, 20, 14, 28, 7, DateTimeKind.Utc).AddTicks(205));
        }
    }
}
