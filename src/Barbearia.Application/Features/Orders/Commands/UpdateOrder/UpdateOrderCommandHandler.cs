using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace Barbearia.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IPersonRepository personRepository, IItemRepository itemRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _personRepository = personRepository;
        _itemRepository = itemRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateOrderCommandResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        UpdateOrderCommandResponse response = new UpdateOrderCommandResponse();

        var orderFromDatabase = await _orderRepository.GetOrderToOrderByIdAsync(request.OrderId);
        if (orderFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("OrderId", new[] { "Order not found in the database." });
            return response;
        }

        var customerFromDatabase = await _personRepository.GetCustomerByIdAsync(request.PersonId);
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

        List<StockHistoryOrder> stockHistoryOrders = new List<StockHistoryOrder>();
        List<Product> Products = new List<Product>();
        List<Appointment> Appointments = new List<Appointment>();

        foreach (int stockHistoriesOrder in request.StockHistoriesOrderId)
        {
            var stockHistoryOrder = await _itemRepository.GetStockHistoryOrderToOrderByIdAsync(stockHistoriesOrder);
            if (stockHistoryOrder == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("stockHistoryOrder", new[] { "stockHistoryOrder not found in the database." });
                return response;
            }
            stockHistoryOrders.Add(stockHistoryOrder!);

        }

        foreach (int products in request.ProductsId)
        {
            var product = await _itemRepository.GetProductByIdAsync(products);
            if (product == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("product", new[] { "product not found in the database." });
                return response;
            }
            Products.Add(product!);

        }

        foreach (int appointments in request.AppointmentsId)
        {
            var appointment = await _itemRepository.GetAppointmentByIdAsync(appointments);
            if (appointment == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("appointment", new[] { "appointment not found in the database." });
                return response;
            }
            Appointments.Add(appointment!);

        }

        _mapper.Map(request, orderFromDatabase);

        foreach (var stockHistoryOrder in stockHistoryOrders)
        {
            orderFromDatabase.StockHistoriesOrder.Add(stockHistoryOrder);
        }
        foreach (var product in Products)
        {
            orderFromDatabase.Products.Add(product);
        }
        foreach (var appointment in Appointments)
        {
            orderFromDatabase.Appointments.Add(appointment);
        }

        try
        {
            orderFromDatabase.ValidateOrder();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Order_Validation", new[] { "Error in order validation" });
            _logger.LogError(ex, "erro de validação em update order");
            return response;
        }

        await _orderRepository.SaveChangesAsync();

        return response;
    }
}