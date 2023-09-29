namespace Barbearia.Domain.Entities;

public class Person
{
    public int PersonId {get;set;}
    public string Name{get;set;} = string.Empty;
    public DateOnly BirthDate{get;set;}
    public int Gender { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Cnpj {get;set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<Address> Addresses {get; set;} = new();
    public List<Telephone> Telephones {get; set;} = new();
    public List<Order> Orders { get; set; } = new();
    public List<Product> Products { get; set; } = new();//não é só Supplier que tem produto?
    public StockHistory? StockHistory { get; set; }
}