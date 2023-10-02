using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Telephones.Query.GetTelephone;

public class GetTelephoneQueryHandler : IRequestHandler<GetTelephoneQuery, IEnumerable<GetTelephoneDto>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetTelephoneQueryHandler(IPersonRepository personRepository, IMapper mapper){
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetTelephoneDto>> Handle(GetTelephoneQuery request, CancellationToken cancellationToken)
    {
        var telephoneFromDatabase = await _personRepository.GetTelephoneAsync(request.PersonId);
        return _mapper.Map<IEnumerable<GetTelephoneDto>>(telephoneFromDatabase);
    }
}