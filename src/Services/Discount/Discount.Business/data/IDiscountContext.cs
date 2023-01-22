using System.Data.Common;

namespace Discount.Business.data
{
    public interface IDiscountContext
    {
        DbConnection GetConnection();
    }
}
