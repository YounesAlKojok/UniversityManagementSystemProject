using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagementSystem.Models
{
    public class DepartmentsCourses
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Department? Department { get; set; }

        public Course? Course { get; set; }
    }
}
