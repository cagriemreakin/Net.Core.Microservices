using Course.Common.ControllerBases;
using Course.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using PhotoStorage.Dtos;

namespace PhotoStorage.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController {

        [HttpPost]
        public async Task<IActionResult> Save(IFormFile formFile, CancellationToken cancellationToken) {

            if (formFile != null && formFile.Length > 0) {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", formFile.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await formFile.CopyToAsync(stream, cancellationToken);
                var returnPath = $"photos/{formFile.FileName}";

                PhotoDto photoDto = new() { Url = returnPath };
                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty", 400));
        }

        public IActionResult Delete(string photoUrl) {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\photos", photoUrl);
            if (!System.IO.File.Exists(path)) {
                return CreateActionResultInstance(Response<NoContent>.Fail("Not Found", 404));
            }
            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
