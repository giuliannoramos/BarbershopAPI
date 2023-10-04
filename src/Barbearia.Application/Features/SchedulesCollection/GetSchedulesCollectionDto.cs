using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Features.SchedulesCollection;

public class GetSchedulesCollectionDto
{
    public int ScheduleId{get;set;}
    public int WorkingDayId{get;set;}
    public WorkingDayDto? WorkingDay{get;set;}
    public int Status{get;set;}
}