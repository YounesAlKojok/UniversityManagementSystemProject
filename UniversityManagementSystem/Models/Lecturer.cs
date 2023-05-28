using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace UniversityManagementSystem.Models
{
    public partial class Lecturer
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string Fname { get; set; } = null!;
        [Display(Name = "Last Name")]
        public string Lname { get; set; } = null!;

        [CustomValidateDob]
        [DataType(DataType.Date)]
        [Required]
        public DateTime? Dob { get; set; }

        [RegularExpression(@"^.+@ums\.com$", ErrorMessage = "The {0} field is not a valid email address.")]
        [Required]
        public string Mail { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public int? FacultyId { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

        public virtual Faculty? Faculty { get; set; }


        // addon methods and new var

        [Display(Name = "Name")]
        public string FullName => $"{Fname} {Lname}";
        public int? CalculatAge()
        {
            if (Dob.HasValue)
            {
                DateTime today = DateTime.Today;
                int age = today.Year - Dob.Value.Year;
                return age;
            }
            return null;
        }

        //public bool HasDepartment() { return DepartmentId.HasValue; }
        public bool HasCourses() { return Courses.Count > 0; }
        public int GetCoursCount() { return Courses.Count; }


        private class CustomValidateDobAttribute : ValidateDobAttribute
        {
        }
    }
}
