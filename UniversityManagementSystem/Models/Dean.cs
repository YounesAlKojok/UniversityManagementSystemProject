using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace UniversityManagementSystem.Models
{
    public partial class Dean
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
        public string Mail { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        [Range(100, 999, ErrorMessage = "The {0} field must be a three-digit number.")]
        public string Office { get; set; } = null!;

        public virtual ICollection<Appoinment> Appoinments { get; set; } = new List<Appoinment>();

        public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

        public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();




        // Add-on method

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
    }
}
