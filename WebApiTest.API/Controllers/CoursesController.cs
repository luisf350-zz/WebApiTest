using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.API.Models;

namespace WebApiTest.API.Controllers
{
    [Route("api/authors/{authorId}/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var coursesForAuthorRepo = _courseLibraryRepository.GetCourses(authorId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesForAuthorRepo));
        }

        [HttpGet("{courseId}", Name = "GetCourseForAuthor")]
        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseForAuthorRepo = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (courseForAuthorRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDto>(courseForAuthorRepo));
        }

        [HttpPost]
        public ActionResult<CourseDto> CreateCourseForAuthor(Guid authorId, CourseForCreationDto course)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseEntity = _mapper.Map<Course>(course);
            _courseLibraryRepository.AddCourse(authorId, courseEntity);
            _courseLibraryRepository.Save();

            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute("GetCourseForAuthor", new { authorId = authorId, courseId = courseToReturn.Id }, courseToReturn);
        }

        [HttpPut("{courseId}")]
        public IActionResult UpdateCourseForAuthor(Guid authorId, Guid courseId, CourseForUpdateDto course)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseForAuthorRepo = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (courseForAuthorRepo == null)
            {
                var courseToAdd = _mapper.Map<Course>(course);
                courseToAdd.Id = courseId;

                _courseLibraryRepository.AddCourse(authorId, courseToAdd);
                _courseLibraryRepository.Save();

                var courseToReturn = _mapper.Map<CourseDto>(courseToAdd);

                return CreatedAtRoute("GetCourseForAuthor", new { authorId, courseId = courseToReturn.Id }, courseToReturn);
            }

            // Map the entity to a CourseForUpdateDto
            // Apply the updated field values to that Dto
            // Map the CourseForUpdateDto back to an entity
            _mapper.Map(course, courseForAuthorRepo);

            _courseLibraryRepository.UpdateCourse(courseForAuthorRepo);
            _courseLibraryRepository.Save();

            return NoContent();
        }

        [HttpPatch("{courseId}")]
        public IActionResult PartiallyUpdateCourseForAuthor(Guid authorId, Guid courseId, JsonPatchDocument<CourseForUpdateDto> patchDocument)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseForAuthorRepo = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (courseForAuthorRepo == null)
            {
                return NotFound();
            }
            var courseToPatch = _mapper.Map<CourseForUpdateDto>(courseForAuthorRepo);
            // TODO: Add validation
            patchDocument.ApplyTo(courseToPatch);

            _mapper.Map(courseToPatch, courseForAuthorRepo);
            _courseLibraryRepository.UpdateCourse(courseForAuthorRepo);
            _courseLibraryRepository.Save();

            return NoContent();
        }
    }
}
