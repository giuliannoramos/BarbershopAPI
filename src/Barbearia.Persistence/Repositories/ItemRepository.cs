using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features;
using Barbearia.Domain.Entities;
using Barbearia.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Persistence.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly ItemContext _context;
    private readonly IMapper _mapper;

    public ItemRepository(ItemContext ItemContext, IMapper mapper)
    {
        _context = ItemContext ?? throw new ArgumentNullException(nameof(OrderContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<(IEnumerable<Product>, PaginationMetadata)> GetAllProductsAsync(string? searchQuery, int pageNumber, int pageSize)
    {
        IQueryable<Product> collection = _context.Products;

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var category = searchQuery.Trim().ToLower();
            collection = collection.Where(
               p => p.ProductCategory != null && p.ProductCategory.Name.ToLower().Contains(category)
            );
        }

        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var productToReturn = await collection
            .OrderBy(p => p.ItemId)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (productToReturn, paginationMetadata);
    } 

    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products
        .Include(p=>p.ProductCategory)
        .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context.Products
        .Include(p=>p.ProductCategory)
        .FirstOrDefaultAsync(p=> p.ItemId == productId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}