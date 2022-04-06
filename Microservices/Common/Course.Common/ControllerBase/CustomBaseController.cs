using Course.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Course.Common.ControllerBases {
    public class CustomBaseController : ControllerBase {
        public IActionResult CreateActionResultInstance<T>(Response<T> response) {
            return new ObjectResult(response) {
                StatusCode = response.StatusCode
            };
        }
    }
}
