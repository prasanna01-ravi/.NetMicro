using Npgsql;
using System.Data.Common;

namespace Discount.API.data
{
    public class DiscountContext: IDiscountContext
    {
        private readonly IConfiguration _configuration;
        private DbConnection? Connection;

        public DiscountContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public DbConnection GetConnection()
        {
            if(Connection == null)
            {
                string connectionString = _configuration.GetConnectionString("default") ?? "";
                Connection = new NpgsqlConnection(_configuration.GetConnectionString("default"));
            }
            return Connection;
        }
    }
}
