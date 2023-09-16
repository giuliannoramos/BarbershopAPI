using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Customers.Commands.RemoveCustomer;

public class RemoveCustomerCommandHandler : IRequestHandler<RemoveCustomerCommand, bool>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public RemoveCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper){
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.Id);

        if(customerFromDatabase == null) return false;

        _customerRepository.RemoveCustomer(customerFromDatabase);

        return await _customerRepository.SaveChangesAsync();
    }
}