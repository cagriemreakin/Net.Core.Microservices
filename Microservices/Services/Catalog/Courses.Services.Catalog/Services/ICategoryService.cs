using Course.Common.Dtos;
using Courses.Services.Catalog.Dtos;

namespace Courses.Services.Catalog.Services {
    public interface ICategoryService {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
