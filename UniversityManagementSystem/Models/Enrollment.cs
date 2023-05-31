using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Models
{
    public partial class Enrollment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public DateTime EnrolDate { get; set; }

        [RegularExpression("^(Winter Semester|Summer Semester)$", ErrorMessage = "Invalid semester. Please Select from list.")]
        public string Semester { get; set; } = null!;

        public int StudentId { get; set; }
        [Range(2024, 2026, ErrorMessage = "Please enter a year between 2024 and 2026.")]
        public int Year { get; set; }

        public virtual Course? Course { get; set; } = null!;

        public virtual Student? Student { get; set; } = null!;
        public virtual Lecturer? Lecturer { get; set; }
    }

}
