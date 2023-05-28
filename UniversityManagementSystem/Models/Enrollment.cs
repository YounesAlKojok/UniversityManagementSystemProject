namespace UniversityManagementSystem.Models
{
    public partial class Enrollment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public DateTime EnrolDate { get; set; }

        public string Semester { get; set; } = null!;

        public int StudentId { get; set; }

        public int Year { get; set; }

        public virtual Course Course { get; set; } = null!;

        public virtual Student Student { get; set; } = null!;
    }

}
