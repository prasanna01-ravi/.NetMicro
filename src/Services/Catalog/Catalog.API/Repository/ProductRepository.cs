using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        public ICatalogContext _context { get; }

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await this._context.Products.Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await this._context.Products.Find(p => p.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await this._context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);

            return await this._context.Products.Find(filter).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await this._context.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var updateResult = await this._context.Products.ReplaceOneAsync(p => p.Id.Equals(product.Id), product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var deletedResult = await this._context.Products.DeleteOneAsync(p => p.Id.Equals(id));

            return deletedResult.IsAcknowledged && deletedResult.DeletedCount > 0;
        }
    }
}
