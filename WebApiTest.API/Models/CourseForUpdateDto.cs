using System.ComponentModel.DataAnnotations;

namespace WebApiTest.API.Models
{
    public class CourseForUpdateDto : CourseForManupulationDto
    {
        [Required(ErrorMessage = "You should fill out a description.")]
        public override string Description { get => base.Description; set => base.Description = value; }
    }
}
