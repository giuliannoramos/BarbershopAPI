using Barbearia.Application.Features;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Contracts.Repositories;

public interface IItemRepository
{
    Task<(IEnumerable<Product>, PaginationMetadata)> GetAllProductsAsync(string? searchQuery, int pageNumber, int pageSize);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    void AddProduct(Product product);
    void DeleteProduct(Product product);
    Task<bool> SaveChangesAsync();
}