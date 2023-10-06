namespace Barbearia.Application.Features.WorkingDays.Commands.UpdateWorkingDay;

public class UpdateWorkingDayCommandResponse : BaseResponse
{
    public UpdateWorkingDayDto Telephone {get;set;} = default!;
}