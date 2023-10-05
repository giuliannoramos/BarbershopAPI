namespace Barbearia.Application.Features.Services.Commands.UpdateService;

public class UpdateServiceCommandResponse : BaseResponse
{
    public UpdateServiceDto Service {get; set;} = default!;    
}