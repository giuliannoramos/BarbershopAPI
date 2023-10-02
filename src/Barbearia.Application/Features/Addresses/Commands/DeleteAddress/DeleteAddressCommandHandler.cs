using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, bool>
{
    private readonly IPersonRepository _personRepository;

    public DeleteAddressCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var customerFromDatabase = await _personRepository.GetCustomerByIdAsync(request.PersonId);

        if(customerFromDatabase == null) return false;

        var addressToDelete = customerFromDatabase.Addresses.FirstOrDefault(a => a.AddressId == request.AddressId);

        if (addressToDelete == null) return false;

        _personRepository.DeleteAddress(customerFromDatabase, addressToDelete);

        return await _personRepository.SaveChangesAsync();
    }
}