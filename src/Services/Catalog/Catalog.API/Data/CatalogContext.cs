using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration) 
        {
            var client = new MongoClient(configuration.GetConnectionString("default"));
            var db = client.GetDatabase(configuration.GetConnectionString("defaultDb"));
            Products = db.GetCollection<Product>(configuration.GetConnectionString("defaultCollection"));
            CatalogContextSeed.SeedData(Products).Wait();
        }
        public IMongoCollection<Product> Products { get; }
    }
}
