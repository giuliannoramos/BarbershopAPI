using MediatR;

namespace Barbearia.Application.Features.WorkingDays.Commands.DeleteWorkingDay;

public class DeleteWorkingDayCommand : IRequest<bool>{
    public int PersonId {get;set;}
    public int WorkingDayId {get;set;}
}