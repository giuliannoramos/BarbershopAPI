using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features;
using Barbearia.Domain.Entities;
using Barbearia.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerContext _context;
    private readonly IMapper _mapper;

    public CustomerRepository(CustomerContext customerContext, IMapper mapper)
    {
        _context = customerContext ?? throw new ArgumentNullException(nameof(customerContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

        public async Task<(IEnumerable<Customer>, PaginationMetadata)> GetAllCustomersAsync(string? searchQuery,
         int pageNumber, int pageSize)
        {
        IQueryable<Customer> collection = _context.Persons.OfType<Customer>()
        .Include(c=>c.Telephones);
        

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var name = searchQuery.Trim();
            collection = collection.Where(

                c => c.Name.Contains(searchQuery)
            );
        }


        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var customerToReturn = await collection
        .OrderBy(c => c.PersonId)
        .Skip(pageSize * (pageNumber-1))
        .Take(pageSize)
        .ToListAsync();

        return (customerToReturn, paginationMetadata);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _context.Persons.OfType<Customer>()
        .Include(c => c.Telephones)
        .OrderBy(p => p.PersonId)
        .ToListAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Persons.OfType<Customer>()
            .Include(c => c.Telephones)
            .Include(c => c.Addresses)
            .FirstOrDefaultAsync(p => p.PersonId == customerId);
    }

    public void AddCustomer(Customer customer)
    {
        _context.Persons.Add(customer);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }    

    public void DeleteCustomer(Customer customer)
    {
        _context.Persons.Remove(customer);
    }

    public async Task<IEnumerable<Address>?> GetAddressAsync(int customerId)
    {
        var customerFromDatabase = await GetCustomerByIdAsync(customerId);
        return customerFromDatabase?.Addresses;
    }

    public void AddAddress(Customer customer, Address address)
    {
        customer.Addresses.Add(address);
    }

    public void DeleteAddress(Customer customer, Address address)
    {
        customer.Addresses.Remove(address);
    }

    public async Task<IEnumerable<Telephone>?> GetTelephoneAsync(int customerId)
    {
        var customerFromDatabase = await GetCustomerByIdAsync(customerId);
        return customerFromDatabase?.Telephones;
    }

    public void AddTelephone(Customer customer, Telephone telephone)
    {
        customer.Telephones.Add(telephone);
    }

    public void DeleteTelephone(Customer customer, Telephone telephone)
    {
        customer.Telephones.Remove(telephone);
    }
    public async Task<Customer?> GetCustomerWithOrdersByIdAsync(int customerId)
    {
        return await _context.Persons.OfType<Customer>()
        .Include(p=>p.Orders)
        .FirstOrDefaultAsync(c=>c.PersonId == customerId);
    }
}