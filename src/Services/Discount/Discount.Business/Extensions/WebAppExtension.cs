using Discount.Business.data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Business.Extensions
{
    public static class WebAppExtension
    {
        public static IHost MigrateDatabase(this IHost app, int retryCount = 0)
        {
            using (var scope = app.Services.CreateScope())
            {
                ILogger logger = scope.ServiceProvider.GetRequiredService<ILogger<IHost>>();

                try
                {
                    logger.LogInformation("Migrating PostgreSql database");

                    IDiscountContext discountContext = scope.ServiceProvider.GetRequiredService<IDiscountContext>();
                    using var connection = discountContext.GetConnection();
                    connection.Open();

                    using var command = new NpgsqlCommand()
                    {
                        Connection = (NpgsqlConnection)connection
                    };

                    command.CommandText = "Drop table if exists coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"Create Table Coupon (Id Serial Primary Key,
                                                            ProductName varchar(24) Not Null,
                                                            Description Text,
                                                            Amount Int)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"Insert into Coupon (ProductName, Description, Amount)
                                        Values ('IPhone X', 'IPhone Discount', 150)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"Insert into Coupon (ProductName, Description, Amount)
                                        Values ('Samsung 20', 'Samsung Discount', 100)";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated PostgreSql database");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error has occured while migrating the postgre sql database");

                    while (retryCount < 5)
                    {
                        retryCount++;
                        Thread.Sleep(3000);
                        MigrateDatabase(app, retryCount);
                    }
                }
            }

            return app;
        }
    }
}
