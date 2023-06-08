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


        //addon methods and new var
        
        /*method uses LINQ's Select method to project each faculty's name from the Faculties collection and converts result
            * into a List<string> using the ToList method.It returns a list of faculty names associated with the department.
           */
        //public List<string> GetDepartmentFacultyNames()
        //{
        //    return Departments.Select(department => department.Name).ToList();
        //}

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
