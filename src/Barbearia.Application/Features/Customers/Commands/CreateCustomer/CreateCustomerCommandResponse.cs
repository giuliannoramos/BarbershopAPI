namespace Barbearia.Application.Features.Customers.Commands;

public class CreateCustomerCommandResponse : BaseResponse
{
    public CreateCustomerDto Customer {get; set;} = default!;    
}