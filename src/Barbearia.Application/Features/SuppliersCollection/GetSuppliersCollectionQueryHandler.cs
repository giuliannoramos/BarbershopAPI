using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.SuppliersCollection;

public class GetSuppliersCollectionQueryHandler : IRequestHandler<GetSuppliersCollectionQuery, GetSuppliersCollectionQueryResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetSuppliersCollectionQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GetSuppliersCollectionQueryResponse> Handle(GetSuppliersCollectionQuery request, CancellationToken cancellationToken)
    {

        GetSuppliersCollectionQueryResponse response = new();

        var (suppliersToReturn, paginationMetadata) = await _customerRepository.GetAllSuppliersAsync(request.SearchQuery, request.PageNumber, request.PageSize);

        var supliersOnly = suppliersToReturn.Where(s => !string.IsNullOrEmpty(s.Cnpj)).ToList();

        response.Suppliers = _mapper.Map<IEnumerable<GetSuppliersCollectionDto>>(supliersOnly);
        response.PaginationMetadata = paginationMetadata;

        return response;
    }
}