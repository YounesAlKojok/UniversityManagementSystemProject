using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Data
{
    //public partial class UniversityManagementSysDbContext : DbContext
    public partial class UniversityManagementSysDbContext : IdentityDbContext
    {
        public UniversityManagementSysDbContext()
        {
        }

        public UniversityManagementSysDbContext(DbContextOptions<UniversityManagementSysDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appoinment> Appoinments { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Dean> Deans { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Enrollment> Enrollments { get; set; }

        public virtual DbSet<Faculty> Faculties { get; set; }

        public virtual DbSet<Grade> Grades { get; set; }

        public virtual DbSet<Lecturer> Lecturers { get; set; }

        public virtual DbSet<Student> Students { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }


        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source= UniversityManagementSysDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder); // Call the base method to configure the identity-related entities

            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.HasKey(e => e.Id);
            });






            modelBuilder.Entity<Appoinment>(entity =>
            {
                entity.ToTable("Appoinment");

                entity.Property(e => e.Date).HasColumnType("Datetime");
                entity.Property(e => e.DeanId).HasColumnName("Dean_Id");
                entity.Property(e => e.Reason).HasColumnType("varchar(100)");
                entity.Property(e => e.StudentId).HasColumnName("Student_Id");

                entity.HasOne(d => d.Dean).WithMany(p => p.Appoinments)
                    .HasForeignKey(d => d.DeanId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Student).WithMany(p => p.Appoinments)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Code).HasColumnType("nvarchar(10)");
                entity.Property(e => e.Description).HasColumnType("varchar(150)");
                entity.Property(e => e.FacultyId).HasColumnName("Faculty_Id");
                entity.Property(e => e.LecturerId).HasColumnName("Lecturer_Id");
                entity.Property(e => e.Name).HasColumnType("varchar(100)");

                entity.HasOne(d => d.Faculty).WithMany(p => p.Courses)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Lecturer).WithMany(p => p.Courses).HasForeignKey(d => d.LecturerId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Departments)
                .WithMany(p => p.Courses)
                    .UsingEntity<DepartmentsCourses>(j => j.HasOne(d => d.Department).WithMany().HasForeignKey("DepartmentId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(d => d.Course).WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("CourseId", "DepartmentId");
                    j.ToTable("DepartmentsCourses");
                });
            });

            modelBuilder.Entity<Dean>(entity =>
            {
                entity.ToTable("Dean");

                entity.Property(e => e.Address).HasColumnType("Nvarchar(150)");
                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");
                entity.Property(e => e.Fname)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("FName");
                entity.Property(e => e.Lname)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("LName");
                entity.Property(e => e.Mail).HasColumnType("varchar(100)");
                entity.Property(e => e.Office).HasColumnType("varchar(3)");
                entity.Property(e => e.Phone).HasColumnType("varchar(15)");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DeanId).HasColumnName("Dean_Id");
                entity.Property(e => e.FacultyId).HasColumnName("Faculty_Id");
                entity.Property(e => e.Name).HasColumnType("varchar(100)");

                entity.HasOne(d => d.Dean).WithMany(p => p.Departments)
                    .HasForeignKey(d => d.DeanId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Faculty).WithMany(p => p.Departments)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("Enrollment");

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");
                entity.Property(e => e.EnrolDate).HasColumnType("datetime");
                entity.Property(e => e.Semester).HasColumnType("varchar(10)");
                entity.Property(e => e.StudentId).HasColumnName("Student_Id");

                entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("Faculty");

                entity.Property(e => e.DeanId).HasColumnName("Dean_Id");
                entity.Property(e => e.Location).HasColumnType("varchar(100)");
                entity.Property(e => e.Name).HasColumnType("varchar(50)");

                entity.HasOne(d => d.Dean).WithMany(p => p.Faculties)
                    .HasForeignKey(d => d.DeanId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasOne(d => d.Course).WithMany(p => p.Grades)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Lecturer>(entity =>
            {
                entity.ToTable("Lecturer");

                entity.Property(e => e.Address).HasColumnType("Nvarchar(150)");
                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");
                entity.Property(e => e.FacultyId).HasColumnName("Faculty_Id");
                entity.Property(e => e.Fname)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("FName");
                entity.Property(e => e.Lname)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("LName");
                entity.Property(e => e.Mail).HasColumnType("varchar(100)");
                entity.Property(e => e.Phone).HasColumnType("varchar(15)");

                entity.HasOne(d => d.Faculty).WithMany(p => p.Lecturers).HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Id).UseCollation("RTRIM");
                entity.Property(e => e.Address).HasColumnType("Nvarchar(150)");
                entity.Property(e => e.DepartmentId).HasColumnName("Department_Id");
                entity.Property(e => e.Dob)
                    //.UseCollation("UTF16")
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");
                entity.Property(e => e.Fname)
                    //.UseCollation("UTF16CI")
                    .HasColumnType("Varchar(100)")
                    .HasColumnName("FName");
                entity.Property(e => e.Lname)
                    .HasColumnType("Varchar(100)")
                    .HasColumnName("LName");
                entity.Property(e => e.Mail).HasColumnType("varchar(100)");
                entity.Property(e => e.Phone).HasColumnType("Varchar(15)");

                entity.HasOne(d => d.Department).WithMany(p => p.Students)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<DepartmentsCourses>(entity =>
            {
                entity.HasKey(e => e.Id); // Specify primary key

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentsCourses)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.DepartmentsCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<UniversityManagementSystem.Models.DepartmentsCourses>? DepartmentsCourses { get; set; }
    }

}
