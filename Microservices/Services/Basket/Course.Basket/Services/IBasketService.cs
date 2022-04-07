using Course.Basket.Dtos;
using Course.Common.Dtos;

namespace Course.Basket.Services {
    public interface IBasketService {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string userId);

    }
}
