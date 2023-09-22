using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbearia.Persistence.Migrations.Order
{
    /// <inheritdoc />
    public partial class NovaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Persons_PersonId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Order_OrderId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_Order_PersonId",
                table: "Orders",
                newName: "IX_Orders_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "CreationDate", "ExpirationDate" },
                values: new object[] { new DateTime(2023, 9, 22, 20, 11, 52, 911, DateTimeKind.Utc).AddTicks(1117), new DateTime(2023, 9, 22, 20, 11, 52, 911, DateTimeKind.Utc).AddTicks(1118) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 9, 22, 20, 11, 52, 911, DateTimeKind.Utc).AddTicks(1046));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 9, 22, 20, 11, 52, 911, DateTimeKind.Utc).AddTicks(1108));

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Persons_PersonId",
                table: "Orders",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Persons_PersonId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PersonId",
                table: "Order",
                newName: "IX_Order_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderId");

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "CreationDate", "ExpirationDate" },
                values: new object[] { new DateTime(2023, 9, 21, 4, 26, 7, 611, DateTimeKind.Utc).AddTicks(5973), new DateTime(2023, 9, 21, 4, 26, 7, 611, DateTimeKind.Utc).AddTicks(5974) });

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 9, 21, 4, 26, 7, 611, DateTimeKind.Utc).AddTicks(5798));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "BuyDate",
                value: new DateTime(2023, 9, 21, 4, 26, 7, 611, DateTimeKind.Utc).AddTicks(5962));

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Persons_PersonId",
                table: "Order",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Order_OrderId",
                table: "Payments",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId");
        }
    }
}
