using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Suppliers.Queries.GetSupplierById;

public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, GetSupplierByIdDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetSupplierByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }
    public async Task<GetSupplierByIdDto> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        var supplierFromDatabase = await _customerRepository.GetSupplierByIdAsync(request.PersonId);

        return _mapper.Map<GetSupplierByIdDto>(supplierFromDatabase);
    }
}