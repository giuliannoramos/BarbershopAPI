namespace Barbearia.Application.Features.Payments.Commands.UpdatePayment;

public class UpdatePaymentCommandResponse : BaseResponse
{
    public UpdatePaymentDto Address {get; set;} = default!;
}