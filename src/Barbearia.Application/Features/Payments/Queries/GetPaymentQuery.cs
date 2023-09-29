using Barbearia.Application.Models;
using MediatR;

namespace Barbearia.Application.Features.Payments.Queries.GetPayment;

public class GetPaymentQuery : IRequest<GetPaymentDto>
{
    public int OrderId {get; set;}
}