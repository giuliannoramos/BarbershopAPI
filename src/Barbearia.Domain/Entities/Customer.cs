namespace Barbearia.Domain.Entities;

public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int Gender { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Address? Address { get; set; }
    public int? AddressId { get; set; }
    public Telephone? Telephone { get; set; }
    public int TelephoneId { get; set; }

    public void ValidateCustomerTelephoneNotNull()
    {
        // Verifica se não há telefone cadastrado
        if (Telephone == null && TelephoneId == null)
        {
            throw new Exception("Um cliente deve conter um telefone.");
        }
    }

    public void ValidateCustomerTelephone()
    {
        // Verifica se já existe um telefone cadastrado
        if (Telephone != null && TelephoneId != null)
        {
            throw new Exception("Um cliente só pode ter um telefone cadastrado.");
        }
    }

    public void ValidateCustomerAddress()
    {
        // Verifica se já existe um endereço cadastrado
        if (Address != null && AddressId != null)
        {
            throw new Exception("Um cliente só pode ter um endereço cadastrado.");
        }
    }
}