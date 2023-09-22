using MediatR;

namespace Barbearia.Application.Features.Telephones.Commands.UpdateTelephone;

public class UpdateTelephoneCommand : IRequest<UpdateTelephoneCommandResponse>
{
    public int PersonId { get; set; }
    public int TelephoneId { get; set; }
    public string Number { get; set; } = string.Empty;    
    public int Type { get; set; }  
}