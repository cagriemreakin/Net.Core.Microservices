using Course.Common.ControllerBases;
using Courses.Services.Catalog.Dtos;
using Courses.Services.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Services.Catalog.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomBaseController {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService) {
            _courseService = courseService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            var courses = await _courseService.GetAllAsync();
            return CreateActionResultInstance(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id) {
            var course = await _courseService.GetByIdAsync(id);
            return CreateActionResultInstance(course);
        }
        [HttpGet]
        [Route("/api/[controller]/GetAllByUserIdAsync/{userId}")]
        public async Task<IActionResult> GetAllByUserIdAsync(string userId) {
            var courses = await _courseService.GetAllByUserIdAsync(userId);
            return CreateActionResultInstance(courses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CourseCreateDto courseCreateDto) {
            var course = await _courseService.CreateAsync(courseCreateDto);
            return CreateActionResultInstance(course);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CourseUpdateDto courseUpdateDto) {
            var course = await _courseService.UpdateAsync(courseUpdateDto);
            return CreateActionResultInstance(course);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id) {
            var course = await _courseService.DeleteAsync(id);
            return CreateActionResultInstance(course);
        }
    }
}
