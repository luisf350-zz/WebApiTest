using AutoMapper;
using CourseLibrary.API.Entities;
using WebApiTest.API.Models;

namespace WebApiTest.API.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<CourseForCreationDto, Course>();
            CreateMap<CourseForUpdateDto, Course>();
        }
    }
}
