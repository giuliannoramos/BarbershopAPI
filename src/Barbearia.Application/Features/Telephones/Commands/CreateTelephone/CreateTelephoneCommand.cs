using MediatR;

namespace Barbearia.Application.Features.Telephones.Commands.CreateTelephone;

public class CreateTelephoneCommand : IRequest<CreateTelephoneCommandResponse>
{
    public int PersonId { get; set; }
    public string Number { get; set; } = string.Empty;    
    public int Type { get; set; }  
}