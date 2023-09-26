using Barbearia.Application.Features.Orders.Commands.DeleteOrder;
using Barbearia.Application.Features.Orders.Commands.UpdateOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Barbearia.Api.Controllers;

[Route("api/orders")]
public class OrdersController : MainController
{
    private readonly IMediator _mediator;
    public OrdersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPut("{orderId}")]
    public async Task<ActionResult> UpdateOrder(int orderId, UpdateOrderCommand updateOrderCommand)
    {
        if (updateOrderCommand.OrderId != orderId) return BadRequest();

        var updateOrderCommandResponse = await _mediator.Send(updateOrderCommand);

        var orderForReturn = updateOrderCommandResponse.Orders;

        if (!updateOrderCommandResponse.IsSuccess)
            return RequestValidationProblem(updateOrderCommandResponse, ModelState);

        if(orderForReturn == null) return StatusCode(500);
        
        return NoContent();
    }

    [HttpDelete("{orderId}")]
    public async Task<ActionResult> DeleteOrder(int orderId)
    {
        var result = await _mediator.Send(new DeleteOrderCommand { OrderId = orderId });

        if (!result) return NotFound();

        return NoContent();
    }
}