using Course.Common.ControllerBases;
using Courses.Services.Catalog.Dtos;
using Courses.Services.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Services.Catalog.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            var categories = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id) {
            var category = await _categoryService.GetByIdAsync(id);
            return CreateActionResultInstance(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryDto categoryDto) {
            var response = await _categoryService.CreateAsync(categoryDto);
            return CreateActionResultInstance(response);
        }
    }
}
