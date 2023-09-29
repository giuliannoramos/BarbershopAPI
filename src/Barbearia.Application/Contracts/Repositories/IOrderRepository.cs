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

    Task<Payment?> GetPaymentAsync(int orderId);
    void AddOrder(Order order);
    void AddPayment(Order order, Payment payment);

    void DeletePayment(Order order, Payment payment);
    void DeleteOrder(Order order);    
    Task<bool> SaveChangesAsync();


}