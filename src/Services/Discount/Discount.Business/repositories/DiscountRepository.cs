using Dapper;
using Discount.Business.data;
using Discount.Business.Entities;
using System.Data.Common;

namespace Discount.Business.repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDiscountContext _discountContext;
        private readonly DbConnection _dbConnection;

        public DiscountRepository(IDiscountContext discountContext)
        {
            _discountContext = discountContext ?? throw new ArgumentNullException(nameof(discountContext));
            _dbConnection = _discountContext.GetConnection();
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            return (await _dbConnection.ExecuteAsync(
                "Insert into coupon (ProductName, Description, Amount) Values " +
                "(@ProductName, @Description, @Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount })) != 0;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            return (await _dbConnection.ExecuteAsync(
                "Delete from Coupon where @ProductName = @ProductName", new { ProductName = productName })) != 0;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            return (await _dbConnection.QueryFirstOrDefaultAsync<Coupon>(
                "Select * from coupon where ProductName=@ProductName", new { ProductName = productName }))
                ?? new Coupon()
                {
                    Amount = 0,
                };
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            return (await _dbConnection.ExecuteAsync(
                "Update coupon set ProductName=@ProductName, Description=@Description, Amount=@Amount " +
                " where Id=@Id",
                new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount,
                    Id = coupon.Id
                })) != 0;
        }
    }
}
