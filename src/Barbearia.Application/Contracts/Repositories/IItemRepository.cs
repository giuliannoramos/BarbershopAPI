using Barbearia.Application.Features;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Contracts.Repositories;

public interface IItemRepository
{
    Task<(IEnumerable<Product>, PaginationMetadata)> GetAllProductsAsync(string? searchQuery, int pageNumber, int pageSize);
}