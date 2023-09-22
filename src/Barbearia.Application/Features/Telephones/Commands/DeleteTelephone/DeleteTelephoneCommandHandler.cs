using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Telephones.Commands.DeleteTelephone;

public class DeleteTelephoneCommandHandler : IRequestHandler<DeleteTelephoneCommand, bool>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteTelephoneCommandHandler(ICustomerRepository customerRepository){
        _customerRepository = customerRepository;
    }
    public async Task<bool> Handle(DeleteTelephoneCommand request, CancellationToken cancellationToken)
    {
        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.PersonId);
        if(customerFromDatabase == null) return false;

        var telefoneFromDatabase = customerFromDatabase.Telephones.FirstOrDefault(t => t.TelephoneId == request.TelefoneId);
        if(telefoneFromDatabase == null) return false;

        _customerRepository.DeleteTelephone(customerFromDatabase, telefoneFromDatabase);

        return await _customerRepository.SaveChangesAsync();
    }
}