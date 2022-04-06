using AutoMapper;
using Course.Common.Dtos;
using Courses.Services.Catalog.Dtos;
using Courses.Services.Catalog.Models;
using Courses.Services.Catalog.Settings;
using MongoDB.Driver;

namespace Courses.Services.Catalog.Services {
    public class CourseService : ICourseService {
        private readonly IMongoCollection<Models.Course> _courseCollection;
        private readonly IMongoCollection<Models.Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings) {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = database.GetCollection<Models.Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Models.Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync() {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any()) {
                foreach (var item in courses) {
                    item.Category = await _categoryCollection.Find<Category>(x => x.Id == item.CategoryId).FirstAsync();
                }
            } else {
                courses = new List<Models.Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id) {
            var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (course == null) {
                return Response<CourseDto>.Fail("Course Not Found", 404);
            }
            course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId) {
            var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();

            if (courses.Any()) {
                foreach (var item in courses) {
                    item.Category = await _categoryCollection.Find<Category>(x => x.Id == item.CategoryId).FirstAsync();
                }
            } else {
                courses = new List<Models.Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }


        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto) {
            var newCourse = _mapper.Map<Models.Course>(courseCreateDto);
            newCourse.CreatedDate = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto) {
            var updateCourse = _mapper.Map<Models.Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);
            if (result == null) {
                return Response<NoContent>.Fail("Course not Found", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id) {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);
            if (result != null && result.DeletedCount > 0) {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Course not Found", 404);
        }

    }
}
