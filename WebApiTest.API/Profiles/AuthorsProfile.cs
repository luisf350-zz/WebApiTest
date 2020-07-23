using AutoMapper;
using CourseLibrary.API.Entities;
using WebApiTest.API.Helpers;
using WebApiTest.API.Models;

namespace WebApiTest.API.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()));
            CreateMap<AuthorForCreationDto, Author>();
        }
    }
}
