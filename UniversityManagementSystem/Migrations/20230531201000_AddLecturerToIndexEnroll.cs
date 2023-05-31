using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementSystem.Migrations
{
    public partial class AddLecturerToIndexEnroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LecturerId",
                table: "Enrollment",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_LecturerId",
                table: "Enrollment",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Lecturer_LecturerId",
                table: "Enrollment",
                column: "LecturerId",
                principalTable: "Lecturer",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Lecturer_LecturerId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_LecturerId",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "Enrollment");
        }
    }
}
