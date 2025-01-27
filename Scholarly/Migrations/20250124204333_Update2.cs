using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholarly.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Teacher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_UserId",
                table: "Teacher",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_student_UserId",
                table: "student",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_student_user_UserId",
                table: "student",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_user_UserId",
                table: "Teacher",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_student_user_UserId",
                table: "student");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_user_UserId",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Teacher_UserId",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_student_UserId",
                table: "student");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "student");
        }
    }
}
