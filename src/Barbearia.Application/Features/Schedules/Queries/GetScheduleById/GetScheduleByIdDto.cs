using Barbearia.Application.Models;

namespace Barbearia.Application.Features.Schedules.Queries.GetScheduleById;

public class GetScheduleByIdDto
{
    public int ScheduleId{get;set;}
    public int WorkingDayId{get;set;}
    public WorkingDayDto? WorkingDay{get;set;}
    public int Status{get;set;}
}