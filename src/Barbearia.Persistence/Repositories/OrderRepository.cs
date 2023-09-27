using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features;
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

    public async Task<(IEnumerable<Order>,PaginationMetadata)> GetAllOrdersAsync(string? searchQuery,
         int pageNumber, int pageSize)
    {
        IQueryable<Order> collection = _context.Orders
        .Include(o=>o.Person);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var name = searchQuery.Trim();
            collection = collection.Where(
                o => o.Number.ToString().Contains(searchQuery)
                || o.Person != null
                && o.Person.Name.ToLower().Contains(name)
            );
        }

        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var orderToReturn = await collection
        .OrderBy(o=>o.OrderId)
        .Skip(pageSize * (pageNumber-1))
        .Take(pageSize)
        .ToListAsync();

        return (orderToReturn, paginationMetadata);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
        .Include(o=>o.Person)
        .OrderBy(o=>o.OrderId)
        .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
        .Include(o=>o.Person)
        .FirstOrDefaultAsync(o=>o.OrderId == orderId);
    }

    public async Task<Order?> GetOrderByNumberAsync(int number)
    {
        return await _context.Orders
        .Include(o=>o.Person)
        .FirstOrDefaultAsync(o=>o.Number == number);
    }

    public void AddOrder(Order order)
    {
        _context.Orders.Add(order);
    }

    public void DeleteOrder(Order order)
    {
        // Excluir pagamentos associados sem cascade
        // var pagamentosParaExcluir = _context.Payments.Where(p => p.OrderId == order.OrderId);
        // _context.Payments.RemoveRange(pagamentosParaExcluir);

        _context.Orders.Remove(order);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}