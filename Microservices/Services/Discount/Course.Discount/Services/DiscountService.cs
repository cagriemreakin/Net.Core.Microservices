using Course.Common.Dtos;
using Dapper;
using Npgsql;
using System.Data;

namespace Course.Discount.Services {
    public class DiscountService : IDiscountService {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration) {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }
        public async Task<Response<List<Models.Discount>>> GetDiscountsAsync() {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }
        public async Task<Response<Models.Discount>> GetByIdAsync(int id) {
            var discount = await _dbConnection.QueryAsync<Models.Discount>("select * from discount where id=@id", new { id });

            if (discount == null) {
                return Response<Models.Discount>.Fail("Dicount not found", 404);
            }
            return Response<Models.Discount>.Success(discount.SingleOrDefault(), 200);
        }
        public async Task<Response<NoContent>> Delete(int id) {
            var discount = await _dbConnection.ExecuteAsync("delete from discount where id=@id", new { id });

            if (discount > 0) {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Dicount not found", 404);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserIdAsync(string code, string userId) {
            var discount = await _dbConnection.QueryAsync<Models.Discount>("select * from discount where userId=@userid and code=@code", new { userId, code });
            if (discount != null) {
                return Response<Models.Discount>.Success(discount.FirstOrDefault(), 200);
            }
            return Response<Models.Discount>.Fail("Not Found", 404);

        }

        public async Task<Response<NoContent>> Save(Models.Discount discount) {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)",
                discount);
            if (saveStatus > 0) {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("No rows added", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount) {
            var response = await GetByIdAsync(discount.Id);
            if (response.StatusCode == 404) {
                return Response<NoContent>.Fail("Not Found", 404);

            }
            var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId,code=@Code,rate=@Rate where id=@Id",
                new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate });
            if (status > 0) {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Error Occured", 500);
        }
    }
}
