namespace Barbearia.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandResponse : BaseResponse
{
    public UpdateCustomerDto Customer {get; set;} = default!;    
}