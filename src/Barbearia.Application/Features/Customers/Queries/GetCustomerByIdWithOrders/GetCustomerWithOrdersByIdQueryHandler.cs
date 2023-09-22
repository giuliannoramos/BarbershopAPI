using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Customers.Queries.GetCustomerWithOrdersById;

public class GetCustomerWithOrdersByIdQueryHandler : IRequestHandler<GetCustomerWithOrdersByIdQuery, GetCustomerWithOrdersByIdDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerWithOrdersByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GetCustomerWithOrdersByIdDto> Handle(GetCustomerWithOrdersByIdQuery request, CancellationToken cancellationToken)
    {
        var customerFromDatabase = await _customerRepository.GetCustomerWithOrdersByIdAsync(request.PersonId);
        return _mapper.Map<GetCustomerWithOrdersByIdDto>(customerFromDatabase);
    }
}
