using AutoMapper;
using Courses.Services.Catalog.Dtos;
using Courses.Services.Catalog.Models;

namespace Courses.Services.Catalog.Mapping {
    public class GeneralMapping : Profile {
        public GeneralMapping() {
            #region Course
            CreateMap<Models.Course, CourseDto>().ReverseMap();
            CreateMap<Models.Course, CourseCreateDto>().ReverseMap();
            CreateMap<Models.Course, CourseUpdateDto>().ReverseMap();
            #endregion

            #region Category
            CreateMap<Category, CategoryDto>().ReverseMap();
            #endregion

            #region Feature
            CreateMap<Feature, FeatureDto>().ReverseMap();
            #endregion
        }
    }
}
