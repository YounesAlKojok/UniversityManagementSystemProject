using System.Diagnostics;

namespace UniversityManagementSystem.Models
{
    public partial class Course
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Code { get; set; }

        public int Credit { get; set; }

        public string? Description { get; set; }

        public int? LecturerId { get; set; }

        public int FacultyId { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public virtual Faculty Faculty { get; set; } = null!;
        public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

        public virtual Lecturer? Lecturer { get; set; }

        public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
    }

}
