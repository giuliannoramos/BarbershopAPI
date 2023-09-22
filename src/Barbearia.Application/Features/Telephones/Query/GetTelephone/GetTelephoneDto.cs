namespace Barbearia.Application.Features.Telephones.Query.GetTelephone;

public class GetTelephoneDto
{
    public int TelephoneId { get; set; }
    public string Number { get; set; } = string.Empty;    
    public int Type { get; set; }    
}