using Course.Basket.Dtos;
using Course.Basket.Services;
using Course.Common.ControllerBases;
using Course.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace Course.Basket.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController {

        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService ;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService) {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketAsync() {

           var basket = await _basketService.GetBasket(_sharedIdentityService.GetUserId);
            return CreateActionResultInstance(basket);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrIpdateBasketAsync(BasketDto basketDto) {

            var basket = await _basketService.SaveOrUpdate(basketDto);
            return CreateActionResultInstance(basket);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync() {

            var response = await _basketService.Delete(_sharedIdentityService.GetUserId);
            return CreateActionResultInstance(response);
        }
    }
}
