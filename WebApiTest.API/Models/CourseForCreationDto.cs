using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiTest.API.Models
{
    public class CourseForCreationDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1500)]
        public string Description { get; set; }

    }
}
