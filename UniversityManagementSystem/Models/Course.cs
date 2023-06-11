using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace UniversityManagementSystem.Models
{
    public partial class Course
    {
        public int Id { get; set; }
        [Display(Name = "Course")]
        public string Name { get; set; } = null!;

        public string? Code { get; set; }
        [Range(0, 7, ErrorMessage = "Valid credits are in range of 3 till 7.")]
        public int Credit { get; set; }

        public string? Description { get; set; }
        [Display(Name ="Day Of Study")]
        public string DayOfStudy { get; set; }
        [Display(Name ="Time Of Study")]
        public string TimeOfStudy { get; set; }

        [Display(Name = "Lecturer")]
        public int? LecturerId { get; set; }
        [Display(Name = "Faculty")]
        [Required(ErrorMessage ="Field is required")]
        public int FacultyId { get; set; }

        public virtual Faculty? Faculty { get; set; } = null!;
        public virtual Lecturer? Lecturer { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
        public virtual ICollection<DepartmentsCourses> DepartmentsCourses { get; set; } = new List<DepartmentsCourses>();
    }

}
