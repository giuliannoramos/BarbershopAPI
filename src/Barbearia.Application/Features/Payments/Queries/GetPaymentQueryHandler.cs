using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Models;
using MediatR;

namespace Barbearia.Application.Features.Payments.Queries.GetPayment;

public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery,GetPaymentDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetPaymentQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<GetPaymentDto> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        var paymentFromDatabase = await _orderRepository.GetPaymentAsync(request.OrderId);
        return _mapper.Map<GetPaymentDto>(paymentFromDatabase);
    }
}