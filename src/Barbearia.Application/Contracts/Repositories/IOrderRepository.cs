using Barbearia.Application.Features;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Contracts.Repositories;

public interface IOrderRepository
{    

    
    Task<(IEnumerable<Order>,PaginationMetadata)> GetAllOrdersAsync(string? searchQuery,
         int pageNumber, int pageSize);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<Order?> GetOrderByNumberAsync(int number);
    void AddOrder(Order order);
    void DeleteOrder(Order order);    
    Task<bool> SaveChangesAsync();


}