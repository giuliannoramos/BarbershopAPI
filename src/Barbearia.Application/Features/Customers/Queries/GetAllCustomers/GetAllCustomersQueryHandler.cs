using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<GetAllCustomersDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAllCustomersDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customerFromDatabase = await _customerRepository.GetAllCustomersAsync();

        return _mapper.Map<IEnumerable<GetAllCustomersDto>>(customerFromDatabase);
    }
}