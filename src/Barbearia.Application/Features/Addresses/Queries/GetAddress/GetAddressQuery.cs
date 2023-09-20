using MediatR;

namespace Barbearia.Application.Features.Addresses.Queries.GetAddress;

public class GetAddressQuery : IRequest<IEnumerable<GetAddressDto>>
{
    public int PersonId { get; set; }
}