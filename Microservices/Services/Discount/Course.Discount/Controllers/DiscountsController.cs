using Course.Common.ControllerBases;
using Course.Common.Services;
using Course.Discount.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Discount.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService) {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var response = await _discountService.GetDiscountsAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)  {
            var response = await _discountService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        [HttpGet("GetByCodeAndUserIdAsync/{code}")]
        public async Task<IActionResult> GetByCodeAndUserIdAsync(string code) {
            var response = await _discountService.GetByCodeAndUserIdAsync(code, _sharedIdentityService.GetUserId);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Models.Discount discount) {
            var response = await _discountService.Save(discount);
            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            var response = await _discountService.Delete(id);
            return CreateActionResultInstance(response);
        }
    }
}
