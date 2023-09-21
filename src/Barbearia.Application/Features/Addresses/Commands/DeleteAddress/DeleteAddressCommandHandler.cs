using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, bool>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteAddressCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.PersonId);

        if(customerFromDatabase == null) return false;

        var addressToDelete = customerFromDatabase.Addresses.FirstOrDefault(a => a.AddressId == request.AddressId);

        if (addressToDelete == null) return false;

        _customerRepository.DeleteAddress(customerFromDatabase, addressToDelete);

        return await _customerRepository.SaveChangesAsync();
    }
}