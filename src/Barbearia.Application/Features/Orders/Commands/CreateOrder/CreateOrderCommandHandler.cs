using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository, IMapper mapper, ILogger<CreateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        CreateOrderCommandResponse response = new();

        var validator = new CreateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.PersonId);

        if (customerFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Customer not found." });
            return response;
        }

        if(!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var orderEntity = _mapper.Map<Order>(request);

        try
        {
            orderEntity.ValidateOrder();
        }
        catch(Exception ex)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Order_Validation", new[] {"Error in order validation"});
            _logger.LogError(ex,"erro de validação em create order");
            return response;
        }

        _orderRepository.AddOrder(orderEntity);
        await _orderRepository.SaveChangesAsync();

        response.Order = _mapper.Map<CreateOrderDto>(orderEntity);
        return response;
    }
}