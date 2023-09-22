namespace Barbearia.Application.Features.Telephones.Commands.UpdateTelephone;

public class UpdateTelephoneDto{
    public int TelephoneId{get;set;}
    public string Number { get; set; } = string.Empty;
    public int Type { get; set; }
}