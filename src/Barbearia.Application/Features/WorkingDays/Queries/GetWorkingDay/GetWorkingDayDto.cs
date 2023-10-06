using Barbearia.Application.Models;

namespace Barbearia.Application.Features.WorkingDays.Query.GetWorkingDay;
public class GetWorkingDayDto
{
    public int WorkingDayId{get; set;}
    public DateOnly WorkDate{get; set;}
    public TimeOnly StartTime{get;set;}
    public TimeOnly FinishTime{get;set;}
    public ScheduleForWorkingDayDto? Schedule{get; set;}    
}