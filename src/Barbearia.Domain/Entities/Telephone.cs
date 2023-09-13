namespace Barbearia.Domain.Entities;

public class Telephone
{
    public int TelephoneId { get; set; }
    public string Number { get; set; } = string.Empty;    
    public int Type { get; set; }
    public int PersonId {get;set;}
    public Person? Person {get;set;}
    
}