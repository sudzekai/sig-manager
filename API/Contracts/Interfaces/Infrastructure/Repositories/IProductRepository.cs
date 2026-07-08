using Domain.Models.Products;
using Domain.ValueObjects.Products;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task<ProductId> AddAsync(Product product);
        Task<bool> DeleteAsync(ProductId id);
        Task<Product?> GetAsync(ProductId id);
        Task<ProductId?> GetIdByNameAsync(ProductName name);
        Task UpdateAsync(Product product);
    }
}
