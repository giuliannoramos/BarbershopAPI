using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Telephones.Query.GetTelephone;

public class GetTelephoneQueryHandler : IRequestHandler<GetTelephoneQuery, IEnumerable<GetTelephoneDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetTelephoneQueryHandler(ICustomerRepository customerRepository, IMapper mapper){
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetTelephoneDto>> Handle(GetTelephoneQuery request, CancellationToken cancellationToken)
    {
        var telephoneFromDatabase = await _customerRepository.GetTelephoneAsync(request.PersonId);
        return _mapper.Map<IEnumerable<GetTelephoneDto>>(telephoneFromDatabase);
    }
}