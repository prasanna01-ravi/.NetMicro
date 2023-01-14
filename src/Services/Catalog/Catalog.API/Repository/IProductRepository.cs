using Catalog.API.Entities;

namespace Catalog.API.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(String id);
        Task<IEnumerable<Product>> GetProductByNameAsync(String name);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(String category);
        Task CreateProduct(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(String id);
    }
}
