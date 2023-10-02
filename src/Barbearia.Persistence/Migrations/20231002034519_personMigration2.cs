using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbearia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class personMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_WorkingDay_WorkingDayId",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "Schedule");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_WorkingDayId",
                table: "Schedule",
                newName: "IX_Schedule_WorkingDayId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule",
                column: "ScheduleId");

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "PersonId",
                keyValue: 3,
                columns: new[] { "Cnpj", "Cpf", "Gender", "Status" },
                values: new object[] { "73473003096986", "", 0, 1 });

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "PersonId",
                keyValue: 4,
                column: "Status",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "PersonId",
                keyValue: 5,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "PersonId",
                keyValue: 6,
                column: "Status",
                value: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_WorkingDay_WorkingDayId",
                table: "Schedule",
                column: "WorkingDayId",
                principalTable: "WorkingDay",
                principalColumn: "WorkingDayId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_WorkingDay_WorkingDayId",
                table: "Schedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule");

            migrationBuilder.RenameTable(
                name: "Schedule",
                newName: "Schedules");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_WorkingDayId",
                table: "Schedules",
                newName: "IX_Schedules_WorkingDayId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "ScheduleId");

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "PersonId",
                keyValue: 3,
                columns: new[] { "Cnpj", "Cpf", "Gender", "Status" },
                values: new object[] { "", "73473943096", 2, 0 });

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "PersonId",
                keyValue: 4,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "PersonId",
                keyValue: 5,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "PersonId",
                keyValue: 6,
                column: "Status",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_WorkingDay_WorkingDayId",
                table: "Schedules",
                column: "WorkingDayId",
                principalTable: "WorkingDay",
                principalColumn: "WorkingDayId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
