using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.WorkingDays.Query.GetWorkingDay;

public class GetWorkingDayQueryHandler : IRequestHandler<GetWorkingDayQuery, IEnumerable<GetWorkingDayDto>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetWorkingDayQueryHandler(IPersonRepository personRepository, IMapper mapper){
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetWorkingDayDto>> Handle(GetWorkingDayQuery request, CancellationToken cancellationToken)
    {
        var employeeFromDatabase = await _personRepository.GetEmployeeByIdAsync(request.PersonId);

        if (employeeFromDatabase == null)
        {
            return null!;
        }

        var workingDaysFromDatabase = await _personRepository.GetWorkingDayAsync(request.PersonId);
        return _mapper.Map<IEnumerable<GetWorkingDayDto>>(workingDaysFromDatabase);
    }
}