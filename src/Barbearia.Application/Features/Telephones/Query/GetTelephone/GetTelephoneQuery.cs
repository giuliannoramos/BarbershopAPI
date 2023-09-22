using MediatR;

namespace Barbearia.Application.Features.Telephones.Query.GetTelephone;

public class GetTelephoneQuery : IRequest<IEnumerable<GetTelephoneDto>>
{
    public int PersonId {get; set;}
}