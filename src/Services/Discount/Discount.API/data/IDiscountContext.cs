using System.Data.Common;

namespace Discount.API.data
{
    public interface IDiscountContext
    {
        DbConnection GetConnection();
    }
}
