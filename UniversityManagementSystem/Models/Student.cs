using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace UniversityManagementSystem.Models
{
    public partial class Student
    {
        public int Id { get; set; }

        public string? Address { get; set; }

        [CustomValidateDob]
        [DataType(DataType.Date)]
        [Required]
        public DateTime? Dob { get; set; }

        public string Fname { get; set; } = null!;

        public string Lname { get; set; } = null!;

        public int DepartmentId { get; set; }

        public string Mail { get; set; } = null!;

        public string? Phone { get; set; }

        public virtual ICollection<Appoinment> Appoinments { get; set; } = new List<Appoinment>();

        public virtual Department Department { get; set; } = null!;

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();


        // addon methods and new var

        [Display(Name = "Name")]
        public string FullName => $"{Fname} {Lname}";

        public int? CalculateAge()
        {
            if (Dob.HasValue)
            {
                DateTime today = DateTime.Today;
                int age = today.Year - Dob.Value.Year;
                return age;
            }
            return null;
        }
        private class CustomValidateDobAttribute : ValidateDobAttribute
        {
        }




        // Method to calculate the average score for the student
        public double CalculateAverageScore()
        {
            if (Grades.Count == 0)
            {
                return 0.0;
            }

            double totalScore = 0.0;
            foreach (var grade in Grades)
            {
                totalScore += Convert.ToDouble(grade.Score);
            }

            return totalScore / Grades.Count;
        }



        // Method to get the list of courses taken by the student
        public List<Course> GetCoursesTaken()
        {
            List<Course> coursesTaken = new List<Course>();
            foreach (var enrollment in Enrollments)
            {
                coursesTaken.Add(enrollment.Course);
            }

            return coursesTaken;
        }
    }
}
