using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;
using Microsoft.VisualBasic;

namespace Barbearia.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<UpdateOrderCommandResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        UpdateOrderCommandResponse response = new UpdateOrderCommandResponse();

        var orderFromDatabase = await _orderRepository.GetOrderByIdAsync(request.OrderId);
        if (orderFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("OrderId", new[] { "Order not found in the database." });
            return response;
        }

        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.PersonId);
        if (customerFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Customer not found in the database." });
            return response;
        }

        var validator = new UpdateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, orderFromDatabase);

        await _orderRepository.SaveChangesAsync();

        return response;
    }
}