using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using Barbearia.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderContext _context;
    private readonly IMapper _mapper;

    public OrderRepository(OrderContext OrderContext, IMapper mapper)
    {
        _context = OrderContext ?? throw new ArgumentNullException(nameof(OrderContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
        .Include(o => o.Person)
        .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public void DeleteOrder(Order order)
    {
        // Excluir pagamentos associados
        var pagamentosParaExcluir = _context.Payments.Where(p => p.OrderId == order.OrderId);
        _context.Payments.RemoveRange(pagamentosParaExcluir);

        _context.Orders.Remove(order);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}