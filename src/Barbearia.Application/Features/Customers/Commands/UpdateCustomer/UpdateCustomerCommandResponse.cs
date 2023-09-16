namespace Barbearia.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandResponse
{
    public bool IsSuccess;
    public bool Exist {get; set;}
    public Dictionary<string, string[]> Errors {get; set;}
    public UpdateCustomerDto Customer {get; set;} = default!;

    public UpdateCustomerCommandResponse(){
        Exist = true;
        IsSuccess = true;
        Errors = new Dictionary<string, string[]>();
    }
}