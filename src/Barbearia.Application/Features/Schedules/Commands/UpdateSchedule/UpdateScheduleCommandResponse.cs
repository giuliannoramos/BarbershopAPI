namespace Barbearia.Application.Features.Schedules.Commands.UpdateSchedule;

public class UpdateScheduleCommandResponse : BaseResponse
{
    public UpdateScheduleDto Schedule {get; set;} = default!;    
}