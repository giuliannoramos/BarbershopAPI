using Barbearia.Domain.Entities;

namespace Barbearia.Application.Contracts.Repositories;

public interface IOrderRepository
{    
    Task<Order?> GetOrderByIdAsync(int orderId);
    void DeleteOrder(Order order);    
    Task<bool> SaveChangesAsync();    
}