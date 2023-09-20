using Barbearia.Domain.Entities;

namespace Barbearia.Application.Contracts.Repositories;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int customerId);
    void AddCustomer(Customer customer);
    void DeleteCustomer(Customer customer);
    Task<bool> SaveChangesAsync();
    Task<IEnumerable<Address>?> GetAddressAsync(int customerId);
    void AddAddress(Customer customer, Address address);    
}