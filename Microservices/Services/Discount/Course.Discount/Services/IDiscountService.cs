using Course.Common.Dtos;

namespace Course.Discount.Services {
    public interface IDiscountService {
        Task<Response<List<Models.Discount>>> GetDiscountsAsync();
        Task<Response<Models.Discount>> GetByIdAsync(int id);
        Task<Response<NoContent>> Save(Models.Discount discount);
        Task<Response<NoContent>> Update(Models.Discount discount);
        Task<Response<NoContent>> Delete(int id);
        Task<Response<Models.Discount>> GetByCodeAndUserIdAsync(string code,string userId);
    }
}