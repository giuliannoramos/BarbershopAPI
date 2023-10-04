using Barbearia.Application.Models;

namespace Barbearia.Application.Features.Schedules.Commands.CreateSchedule;

public class CreateScheduleDto
{
    public int ScheduleId{get;set;}
    public int WorkingDayId{get;set;}
    public int Status{get;set;}
}