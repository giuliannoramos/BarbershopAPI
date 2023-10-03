namespace Barbearia.Application.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandResponse : BaseResponse
{
    public UpdateEmployeeDto Employee {get; set;} = default!;    
}