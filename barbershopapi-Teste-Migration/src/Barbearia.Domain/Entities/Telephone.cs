namespace Barbearia.Domain.Entities;

public class Telephone
{
    public int TelephoneId { get; set; }
    public string Number { get; set; } = string.Empty;    
    public int Type { get; set; }
    public Customer? Customer {get;set;}
    public int CustomerId {get;set;}
}