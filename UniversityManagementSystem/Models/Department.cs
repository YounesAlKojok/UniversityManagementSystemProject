using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace UniversityManagementSystem.Models
{
    public partial class Department
    {
        public int Id { get; set; }
        [Display(Name = "Department")]
        public string Name { get; set; } = null!;
        [Display(Name = "Dean")]
        public int DeanId { get; set; }
        [Display(Name = "faculty")]
        public int FacultyId { get; set; }

        public virtual Dean? Dean { get; set; } = null!;

        public virtual Faculty? Faculty { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<DepartmentsCourses> DepartmentsCourses { get; set; } = new List<DepartmentsCourses>();


        //addon methods and new var

       
        public int GetCourseCount() { return DepartmentsCourses.Count; }
        public int GetStudentCount() { return Students.Count; }
    }
}
