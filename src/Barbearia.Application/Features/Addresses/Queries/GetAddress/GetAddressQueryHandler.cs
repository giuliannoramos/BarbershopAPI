using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Addresses.Queries.GetAddress;

public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, IEnumerable<GetAddressDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAddressQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<GetAddressDto>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        var addressFromDatabase = await _customerRepository.GetAddressAsync(request.PersonId);
        return _mapper.Map<IEnumerable<GetAddressDto>>(addressFromDatabase);
    }
}