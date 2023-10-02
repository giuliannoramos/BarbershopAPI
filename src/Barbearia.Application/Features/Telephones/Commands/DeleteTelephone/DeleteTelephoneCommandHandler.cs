using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Telephones.Commands.DeleteTelephone;

public class DeleteTelephoneCommandHandler : IRequestHandler<DeleteTelephoneCommand, bool>
{
    private readonly IPersonRepository _personRepository;

    public DeleteTelephoneCommandHandler(IPersonRepository personRepository){
        _personRepository = personRepository;
    }
    public async Task<bool> Handle(DeleteTelephoneCommand request, CancellationToken cancellationToken)
    {
        var customerFromDatabase = await _personRepository.GetCustomerByIdAsync(request.PersonId);
        if(customerFromDatabase == null) return false;

        var telefoneFromDatabase = customerFromDatabase.Telephones.FirstOrDefault(t => t.TelephoneId == request.TelefoneId);
        if(telefoneFromDatabase == null) return false;

        _personRepository.DeleteTelephone(customerFromDatabase, telefoneFromDatabase);

        return await _personRepository.SaveChangesAsync();
    }
}