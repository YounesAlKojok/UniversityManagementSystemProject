using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Models
{
    public partial class Enrollment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        [Display(Name = "Enrollment Date")]
        public DateTime EnrolDate { get; set; }

        [RegularExpression("^(Winter Semester|Summer Semester)$", ErrorMessage = "Invalid semester. Please Select from list.")]
        public string Semester { get; set; } = null!;
        [Display(Name = "Student")]
        public int StudentId { get; set; }
        [Range(2024, 2026, ErrorMessage = "Please enter a year between 2024 and 2026.")]
        public int Year { get; set; }
        [Range(2, 5, ErrorMessage = "Invalid value. Must be 2 till 5.")]
        public Double? Grade { get; set; } = 0.0; // New grade field

        public virtual Course? Course { get; set; } = null!;

        public virtual Student? Student { get; set; } = null!;
        public virtual Lecturer? Lecturer { get; set; }


    }

}
