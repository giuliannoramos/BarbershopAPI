namespace Barbearia.Application.Features.Telephones.Commands.UpdateTelephone;

public class UpdateTelephoneCommandResponse : BaseResponse
{
    public UpdateTelephoneDto Telephone {get;set;} = default!;
}