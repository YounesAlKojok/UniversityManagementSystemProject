﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityManagementSystem.Data;

#nullable disable

namespace UniversityManagementSystem.Migrations
{
    [DbContext(typeof(UniversityManagementSysDbContext))]
    [Migration("20230527061950_IntialUMSMig")]
    partial class IntialUMSMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.16");

            modelBuilder.Entity("DepartmentsCourse", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Course_Id");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Department_Id");

                    b.HasKey("CourseId", "DepartmentId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("DepartmentsCourses", (string)null);
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Appoinment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Datetime");

                    b.Property<int>("DeanId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Dean_Id");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSlotActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Reason")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Student_Id");

                    b.HasKey("Id");

                    b.HasIndex("DeanId");

                    b.HasIndex("StudentId");

                    b.ToTable("Appoinment", (string)null);
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Credit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("FacultyId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Faculty_Id");

                    b.Property<int?>("LecturerId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Lecturer_Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("LecturerId");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Dean", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("Nvarchar(150)");

                    b.Property<DateTime?>("Dob")
                        .IsRequired()
                        .HasColumnType("datetime")
                        .HasColumnName("DOB");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("FName");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("LName");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Office")
                        .IsRequired()
                        .HasColumnType("varchar(3)");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Dean", (string)null);
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeanId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Dean_Id");

                    b.Property<int>("FacultyId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Faculty_Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("DeanId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Department", (string)null);
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Course_Id");

                    b.Property<DateTime>("EnrolDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Student_Id");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollment", (string)null);
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeanId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Dean_Id");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DeanId");

                    b.ToTable("Faculty", (string)null);
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Lecturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("Nvarchar(150)");

                    b.Property<DateTime?>("Dob")
                        .IsRequired()
                        .HasColumnType("datetime")
                        .HasColumnName("DOB");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Faculty_Id");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("FName");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("LName");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Lecturer", (string)null);
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .UseCollation("RTRIM");

                    b.Property<string>("Address")
                        .HasColumnType("Nvarchar(150)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Department_Id");

                    b.Property<DateTime?>("Dob")
                        .IsRequired()
                        .HasColumnType("datetime")
                        .HasColumnName("DOB");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("FName");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("LName");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Phone")
                        .HasColumnType("Varchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Student", (string)null);
                });

            modelBuilder.Entity("DepartmentsCourse", b =>
                {
                    b.HasOne("UniversityManagementSystem.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityManagementSystem.Models.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Appoinment", b =>
                {
                    b.HasOne("UniversityManagementSystem.Models.Dean", "Dean")
                        .WithMany("Appoinments")
                        .HasForeignKey("DeanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityManagementSystem.Models.Student", "Student")
                        .WithMany("Appoinments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dean");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Course", b =>
                {
                    b.HasOne("UniversityManagementSystem.Models.Faculty", "Faculty")
                        .WithMany("Courses")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityManagementSystem.Models.Lecturer", "Lecturer")
                        .WithMany("Courses")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Faculty");

                    b.Navigation("Lecturer");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Department", b =>
                {
                    b.HasOne("UniversityManagementSystem.Models.Dean", "Dean")
                        .WithMany("Departments")
                        .HasForeignKey("DeanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityManagementSystem.Models.Faculty", "Faculty")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dean");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Enrollment", b =>
                {
                    b.HasOne("UniversityManagementSystem.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityManagementSystem.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Faculty", b =>
                {
                    b.HasOne("UniversityManagementSystem.Models.Dean", "Dean")
                        .WithMany("Faculties")
                        .HasForeignKey("DeanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dean");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Grade", b =>
                {
                    b.HasOne("UniversityManagementSystem.Models.Course", "Course")
                        .WithMany("Grades")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityManagementSystem.Models.Student", "Student")
                        .WithMany("Grades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Lecturer", b =>
                {
                    b.HasOne("UniversityManagementSystem.Models.Faculty", "Faculty")
                        .WithMany("Lecturers")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Student", b =>
                {
                    b.HasOne("UniversityManagementSystem.Models.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Course", b =>
                {
                    b.Navigation("Enrollments");

                    b.Navigation("Grades");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Dean", b =>
                {
                    b.Navigation("Appoinments");

                    b.Navigation("Departments");

                    b.Navigation("Faculties");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Department", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Faculty", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Departments");

                    b.Navigation("Lecturers");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Lecturer", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("UniversityManagementSystem.Models.Student", b =>
                {
                    b.Navigation("Appoinments");

                    b.Navigation("Enrollments");

                    b.Navigation("Grades");
                });
#pragma warning restore 612, 618
        }
    }
}
