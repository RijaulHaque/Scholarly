using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholarly.Migrations
{
    /// <inheritdoc />
    public partial class Update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AttendanceData",
                table: "Attendance",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Attendance",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "Attendance",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "Attendance");

            migrationBuilder.AlterColumn<bool>(
                name: "AttendanceData",
                table: "Attendance",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
