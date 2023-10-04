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

    public Task<IEnumerable<StockHistory>> GetAllStockHistoriesAsync();
    public Task<StockHistory?> GetStockHistoryByIdAsync(int stockHistoryId);
    public Task<StockHistoryOrder?> GetStockHistoryOrderByIdAsync(int stockHistoryId);
    public Task<StockHistorySupplier?> GetStockHistorySupplierByIdAsync(int stockHistoryId);
    public void AddStockHistory(StockHistory stockHistory);
    public void RemoveStockHistory(StockHistory stockHistory);
    Task<bool> SaveChangesAsync();

    public Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync();
    public Task<ProductCategory?> GetProductCategoryByIdAsync(int productCategoryId);
    public void AddProductCategory(ProductCategory productCategory);
    public void RemoveProductCategory(ProductCategory productCategory);
}