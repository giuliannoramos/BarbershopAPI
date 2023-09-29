using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Payments.Commands.DeletePayment;

public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public DeletePaymentCommandHandler(IOrderRepository orderRepository){
        _orderRepository = orderRepository;
    }
    public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var orderFromDatabase = await _orderRepository.GetOrderByIdAsync(request.OrderId);
        if(orderFromDatabase == null) return false;

        var paymentFromDatabase = orderFromDatabase.Payment;
        if(paymentFromDatabase == null) return false;

        _orderRepository.DeletePayment(orderFromDatabase, paymentFromDatabase);

        return await _orderRepository.SaveChangesAsync();
    }
}