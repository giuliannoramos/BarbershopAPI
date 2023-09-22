namespace Barbearia.Application.Features.Telephones.Commands.CreateTelephone;

public class CreateTelephoneDto
{
    public int TelephoneId { get; set; }
    public string Number { get; set; } = string.Empty;    
    public int Type { get; set; }    
}