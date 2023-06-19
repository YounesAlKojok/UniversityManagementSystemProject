using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace UniversityManagementSystem.Models
{
    public partial class Faculty
    {
        public int Id { get; set; }
        [Display(Name = "Dean")]
        public int DeanId { get; set; }

        public string Location { get; set; } = null!;
        [Display(Name = "Faculty")]
        public string Name { get; set; } = null!;


        public virtual Dean? Dean { get; set; } = null!;
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

        public virtual ICollection<Lecturer> Lecturers { get; set; } = new List<Lecturer>();


        

        public string GetDepartmentFacultyNames()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var department in Departments)
            {
                sb.AppendLine(department.Name);
            }
            return sb.ToString();
        }

        public int GetCourseCount() { return Courses.Count; }
    }
}
