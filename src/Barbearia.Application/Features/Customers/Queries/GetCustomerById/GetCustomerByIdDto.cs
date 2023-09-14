namespace Barbearia.Application.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdDto 
{
    public int PersonId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
}