namespace Barbearia.Application.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandResponse : BaseResponse
{
    public UpdateAddressDto Address {get; set;} = default!;
}