using Barbearia.Domain.Entities;

namespace Barbearia.Application.Contracts.Repositories;

public interface IOrderRepository
{    
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<Order?> GetOrderByNumberAsync(int number);
    void DeleteOrder(Order order);    
    Task<bool> SaveChangesAsync();


}