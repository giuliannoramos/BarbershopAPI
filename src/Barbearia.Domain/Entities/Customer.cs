namespace Barbearia.Domain.Entities;

public class Customer{
    public int CustomerId {get; set;}
    public string Name {get; set;} = string.Empty;
    public int Gender {get; set;}
    public string Cpf {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public DateOnly BirthDate;
}