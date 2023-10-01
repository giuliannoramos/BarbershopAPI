using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Suppliers.Commands.DeleteSupplier;

public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, bool>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public DeleteSupplierCommandHandler(ICustomerRepository customerRepository, IMapper mapper){
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplierFromDatabase = await _customerRepository.GetSupplierByIdAsync(request.PersonId);

        if(supplierFromDatabase == null) return false;

        _customerRepository.DeleteSupplier(supplierFromDatabase);

        return await _customerRepository.SaveChangesAsync();
    }
}