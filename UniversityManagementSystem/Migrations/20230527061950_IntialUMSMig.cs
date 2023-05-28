using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementSystem.Migrations
{
    public partial class IntialUMSMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dean",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FName = table.Column<string>(type: "varchar(100)", nullable: false),
                    LName = table.Column<string>(type: "varchar(100)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime", nullable: false),
                    Mail = table.Column<string>(type: "varchar(100)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(15)", nullable: true),
                    Address = table.Column<string>(type: "Nvarchar(150)", nullable: true),
                    Office = table.Column<string>(type: "varchar(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dean", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Dean_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Location = table.Column<string>(type: "varchar(100)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculty_Dean_Dean_Id",
                        column: x => x.Dean_Id,
                        principalTable: "Dean",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Dean_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Faculty_Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Dean_Dean_Id",
                        column: x => x.Dean_Id,
                        principalTable: "Dean",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Department_Faculty_Faculty_Id",
                        column: x => x.Faculty_Id,
                        principalTable: "Faculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lecturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FName = table.Column<string>(type: "varchar(100)", nullable: false),
                    LName = table.Column<string>(type: "varchar(100)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime", nullable: false),
                    Mail = table.Column<string>(type: "varchar(100)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(15)", nullable: true),
                    Address = table.Column<string>(type: "Nvarchar(150)", nullable: true),
                    Faculty_Id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecturer_Faculty_Faculty_Id",
                        column: x => x.Faculty_Id,
                        principalTable: "Faculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false, collation: "RTRIM")
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "Nvarchar(150)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime", nullable: false),
                    FName = table.Column<string>(type: "Varchar(100)", nullable: false),
                    LName = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Department_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Mail = table.Column<string>(type: "varchar(100)", nullable: false),
                    Phone = table.Column<string>(type: "Varchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Department_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Credit = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "varchar(150)", nullable: true),
                    Lecturer_Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Faculty_Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Course_Faculty_Faculty_Id",
                        column: x => x.Faculty_Id,
                        principalTable: "Faculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Course_Lecturer_Lecturer_Id",
                        column: x => x.Lecturer_Id,
                        principalTable: "Lecturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appoinment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Dean_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    Reason = table.Column<string>(type: "varchar(100)", nullable: true),
                    Student_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSlotActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appoinment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appoinment_Dean_Dean_Id",
                        column: x => x.Dean_Id,
                        principalTable: "Dean",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appoinment_Student_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentsCourses",
                columns: table => new
                {
                    Course_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Department_Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentsCourses", x => new { x.Course_Id, x.Department_Id });
                    table.ForeignKey(
                        name: "FK_DepartmentsCourses_Course_Course_Id",
                        column: x => x.Course_Id,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentsCourses_Department_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Course_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    EnrolDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Semester = table.Column<string>(type: "varchar(10)", nullable: false),
                    Student_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollment_Course_Course_Id",
                        column: x => x.Course_Id,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollment_Student_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appoinment_Dean_Id",
                table: "Appoinment",
                column: "Dean_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Appoinment_Student_Id",
                table: "Appoinment",
                column: "Student_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Faculty_Id",
                table: "Course",
                column: "Faculty_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Lecturer_Id",
                table: "Course",
                column: "Lecturer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Dean_Id",
                table: "Department",
                column: "Dean_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Faculty_Id",
                table: "Department",
                column: "Faculty_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsCourses_Department_Id",
                table: "DepartmentsCourses",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_Course_Id",
                table: "Enrollment",
                column: "Course_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_Student_Id",
                table: "Enrollment",
                column: "Student_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Faculty_Dean_Id",
                table: "Faculty",
                column: "Dean_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CourseId",
                table: "Grades",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturer_Faculty_Id",
                table: "Lecturer",
                column: "Faculty_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Department_Id",
                table: "Student",
                column: "Department_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appoinment");

            migrationBuilder.DropTable(
                name: "DepartmentsCourses");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Lecturer");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Faculty");

            migrationBuilder.DropTable(
                name: "Dean");
        }
    }
}
