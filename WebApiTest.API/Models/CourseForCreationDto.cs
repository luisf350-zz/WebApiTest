using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiTest.API.ValidationAttributes;

namespace WebApiTest.API.Models
{
    [CourseTitleMustBeDifferentFromDescriptionAttribute]
    public class CourseForCreationDto //: IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1500)]
        public string Description { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description)
        //    {
        //        yield return new ValidationResult("The provided description should be different from the title.",
        //            new[] { "CourseForCreationDto" });
        //    }
        //}
    }
}
