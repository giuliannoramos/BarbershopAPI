namespace Barbearia.Domain.Entities;

public class Telephone
{
    public int TelephoneId { get; set; }
    public string Number { get; set; } = string.Empty;    
    public int Type { get; set; }
    public int PersonId {get;set;}
    public Person? Person {get;set;}

    private void CheckNumber()
    {
        if (!(Number.Length == 11 && Number.All(char.IsDigit)))
        {
            throw new ArgumentException("Número de telefone inválido. Use o formato: 47988887777.");
        }
    }

    public void ValidateTelephone()
    {
        CheckNumber();
    }
    
}