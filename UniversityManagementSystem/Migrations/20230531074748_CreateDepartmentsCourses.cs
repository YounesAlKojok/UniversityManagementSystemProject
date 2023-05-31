using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementSystem.Migrations
{
    public partial class CreateDepartmentsCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsCourses_Course_Course_Id",
                table: "DepartmentsCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsCourses_Department_Department_Id",
                table: "DepartmentsCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentsCourses",
                table: "DepartmentsCourses");

            migrationBuilder.RenameColumn(
                name: "Department_Id",
                table: "DepartmentsCourses",
                newName: "DepartmentId");

            migrationBuilder.RenameColumn(
                name: "Course_Id",
                table: "DepartmentsCourses",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentsCourses_Department_Id",
                table: "DepartmentsCourses",
                newName: "IX_DepartmentsCourses_DepartmentId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DepartmentsCourses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentsCourses",
                table: "DepartmentsCourses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsCourses_CourseId",
                table: "DepartmentsCourses",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentsCourses_Course_CourseId",
                table: "DepartmentsCourses",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentsCourses_Department_DepartmentId",
                table: "DepartmentsCourses",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsCourses_Course_CourseId",
                table: "DepartmentsCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsCourses_Department_DepartmentId",
                table: "DepartmentsCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentsCourses",
                table: "DepartmentsCourses");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentsCourses_CourseId",
                table: "DepartmentsCourses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DepartmentsCourses");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "DepartmentsCourses",
                newName: "Department_Id");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "DepartmentsCourses",
                newName: "Course_Id");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentsCourses_DepartmentId",
                table: "DepartmentsCourses",
                newName: "IX_DepartmentsCourses_Department_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentsCourses",
                table: "DepartmentsCourses",
                columns: new[] { "Course_Id", "Department_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentsCourses_Course_Course_Id",
                table: "DepartmentsCourses",
                column: "Course_Id",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentsCourses_Department_Department_Id",
                table: "DepartmentsCourses",
                column: "Department_Id",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
