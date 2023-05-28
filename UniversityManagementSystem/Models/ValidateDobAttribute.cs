using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Models
{
    public class ValidateDobAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dob)
            {
                var acceptableAge = GetAcceptableAge(validationContext.ObjectType);
                DateTime today = DateTime.Today;
                DateTime minDate = today.AddYears(-acceptableAge);

                if (dob > minDate)
                {
                    return new ValidationResult(ErrorMessage = $"The DOB must be at least {acceptableAge} years ago.");
                }
            }

            return ValidationResult.Success;
        }

        private int GetAcceptableAge(Type objectType)
        {
            if (objectType == typeof(Student))
            {
                return 16;
            }
            else if (objectType == typeof(Dean))
            {
                return 30;
            }
            else if (objectType == typeof(Lecturer))
            {
                return 25;
            }

            // Default acceptable age
            return 0;
        }
    }
}
