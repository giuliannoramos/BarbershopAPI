namespace Barbearia.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandResponse : BaseResponse
{
    public UpdateOrderDto Orders {get; set;} = default!;
}