using MediatR;

namespace Barbearia.Application.Features.WorkingDays.Query.GetWorkingDay;

public class GetWorkingDayQuery : IRequest<IEnumerable<GetWorkingDayDto>>
{
    public int PersonId {get; set;}
}