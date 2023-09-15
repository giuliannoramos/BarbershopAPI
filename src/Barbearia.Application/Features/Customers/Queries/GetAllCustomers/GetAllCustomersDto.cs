namespace Barbearia.Application.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersDto 
{
    public int PersonId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}