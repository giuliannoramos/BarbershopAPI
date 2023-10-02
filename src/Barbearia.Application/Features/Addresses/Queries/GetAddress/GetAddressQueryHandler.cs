using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Addresses.Queries.GetAddress;

public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, IEnumerable<GetAddressDto>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetAddressQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<GetAddressDto>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        var addressFromDatabase = await _personRepository.GetAddressAsync(request.PersonId);
        return _mapper.Map<IEnumerable<GetAddressDto>>(addressFromDatabase);
    }
}