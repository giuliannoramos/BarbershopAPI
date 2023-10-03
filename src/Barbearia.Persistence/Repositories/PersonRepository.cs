using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features;
using Barbearia.Domain.Entities;
using Barbearia.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PersonContext _context;
    private readonly IMapper _mapper;

    public PersonRepository(PersonContext personContext, IMapper mapper)
    {
        _context = personContext ?? throw new ArgumentNullException(nameof(personContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<(IEnumerable<Customer>, PaginationMetadata)> GetAllCustomersAsync(string? searchQuery,
     int pageNumber, int pageSize)
    {
        IQueryable<Customer> collection = _context.Persons.OfType<Customer>()
        .Include(c => c.Telephones);


        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var name = searchQuery.Trim().ToLower();
            collection = collection.Where(

                c => c.Name.ToLower().Contains(name)
            );
        }


        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var customerToReturn = await collection
        .OrderBy(c => c.PersonId)
        .Skip(pageSize * (pageNumber - 1))
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

    public async Task<Supplier?> GetSupplierByIdAsync(int supplierId)
    {
        return await _context.Persons.OfType<Supplier>()
        .Include(s => s.Telephones)
        .Include(s => s.Addresses)
        .Include(s => s.Products)
        .Include(s => s.StockHistories)
        .FirstOrDefaultAsync(s => s.PersonId == supplierId);
    }

    public async Task<(IEnumerable<Supplier>, PaginationMetadata)> GetAllSuppliersAsync(string? searchQuery,
    int pageNumber, int pageSize)
    {
        IQueryable<Supplier> collection = _context.Persons.OfType<Supplier>()
        .Include(c => c.Telephones);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var name = searchQuery.Trim().ToLower();
            collection = collection.Where(

                c => c.Name.ToLower().Contains(name)
            );
        }

        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var SupplierToReturn = await collection
        .OrderBy(c => c.PersonId)
        .Skip(pageSize * (pageNumber - 1))
        .Take(pageSize)
        .ToListAsync();

        return (SupplierToReturn, paginationMetadata);
    }

    public void AddSupplier(Supplier supplier)
    {
        _context.Persons.Add(supplier);
    }

    public void DeleteSupplier(Supplier supplier)
    {
        _context.Persons.Remove(supplier);
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
        .Include(c => c.Orders)
        .FirstOrDefaultAsync(c => c.PersonId == customerId);
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
    {
        return await _context.Persons.OfType<Employee>()
            .Include(c => c.Telephones)
            .Include(c => c.Addresses)
            .FirstOrDefaultAsync(p => p.PersonId == employeeId);
    }

    public void AddEmployee(Employee employee)
    {
        _context.Persons.Add(employee);
    }

    public void DeleteEmployee(Employee employee)
    {
        _context.Persons.Remove(employee);
    }

    public async Task<(IEnumerable<Employee>, PaginationMetadata)> GetAllEmployeesAsync(string? searchQuery,
         int pageNumber, int pageSize)
    {
        IQueryable<Employee> collection = _context.Persons.OfType<Employee>()
        .Include(c => c.Telephones);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var name = searchQuery.Trim().ToLower();
            collection = collection.Where(

                c => c.Name.ToLower().Contains(name)
            );
        }

        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var employeeToReturn = await collection
        .OrderBy(c => c.PersonId)
        .Skip(pageSize * (pageNumber - 1))
        .Take(pageSize)
        .ToListAsync();

        return (employeeToReturn, paginationMetadata);
    }

}