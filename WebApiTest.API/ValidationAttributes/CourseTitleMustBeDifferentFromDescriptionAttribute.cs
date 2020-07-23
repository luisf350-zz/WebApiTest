using System.ComponentModel.DataAnnotations;
using WebApiTest.API.Models;

namespace WebApiTest.API.ValidationAttributes
{
    public class CourseTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (CourseForManupulationDto)validationContext.ObjectInstance;

            if (course.Title == course.Description)
            {
                return new ValidationResult(ErrorMessage,
                    new[] { "CourseForManupulationDto" });
            }

            return ValidationResult.Success;
        }
    }
}
