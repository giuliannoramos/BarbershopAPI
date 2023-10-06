using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.WorkingDays.Commands.DeleteWorkingDay;

public class DeleteWorkingDayCommandHandler : IRequestHandler<DeleteWorkingDayCommand, bool>
{
    private readonly IPersonRepository _personRepository;

    public DeleteWorkingDayCommandHandler(IPersonRepository personRepository){
        _personRepository = personRepository;
    }
    public async Task<bool> Handle(DeleteWorkingDayCommand request, CancellationToken cancellationToken)
    {
        var employeeFromDatabase = await _personRepository.GetEmployeeByIdAsync(request.PersonId);
        if(employeeFromDatabase == null) return false;

        var workingDayFromDatabase = employeeFromDatabase.WorkingDays.FirstOrDefault(w => w.WorkingDayId == request.WorkingDayId);
        if(workingDayFromDatabase == null) return false;

        _personRepository.DeleteWorkingDay(employeeFromDatabase, workingDayFromDatabase);

        return await _personRepository.SaveChangesAsync();
    }
}