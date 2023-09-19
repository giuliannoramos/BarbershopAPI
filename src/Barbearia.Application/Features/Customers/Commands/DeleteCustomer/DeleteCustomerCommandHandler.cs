using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper){
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.Id);

        if(customerFromDatabase == null) return false;

        _customerRepository.DeleteCustomer(customerFromDatabase);

        return await _customerRepository.SaveChangesAsync();
    }
}