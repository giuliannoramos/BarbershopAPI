namespace Barbearia.Domain.Entities;

public class Address
{
    public int AddressId { get; set; }
    public string Street { get; set; } = string.Empty;    
    public int Number { get; set; }
    public string District { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
    public int PersonId { get; set; }
    public Person? Person { get; set; }    
}