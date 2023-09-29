using Barbearia.Application.Features.Payments.Commands.CreatePayment;
using Barbearia.Application.Features.Payments.Commands.DeletePayment;
using Barbearia.Application.Features.Payments.Commands.UpdatePayment;
using Barbearia.Application.Features.Payments.Queries.GetPayment;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/order/{orderId}/payment")]
public class PaymentController : MainController
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator){
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet(Name ="GetPayment")]
    public async Task<ActionResult<PaymentDto>>GetPayment(int orderId)
    {
        var getPaymentQuery = new GetPaymentQuery{OrderId = orderId};
        var paymentToReturn = await _mediator.Send(getPaymentQuery);

        if(paymentToReturn == null) return NotFound();

        return Ok(paymentToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreatePaymentCommandResponse>> CreatePayment(int orderId, CreatePaymentCommand createPaymentCommand)
    {
        if(orderId != createPaymentCommand.OrderId) return BadRequest();

        var createPaymentCommandResponse = await _mediator.Send(createPaymentCommand);

        if(!createPaymentCommandResponse.IsSuccess)
            return RequestValidationProblem(createPaymentCommandResponse, ModelState);

        var PaymentForReturn = createPaymentCommandResponse.Payment;


        return CreatedAtRoute
        (
            "GetPayment",
            new { orderId = PaymentForReturn.OrderId },
            PaymentForReturn
        );
    }



    [HttpDelete ("{paymentId}")]
    public async Task<ActionResult> DeletePayment(int orderId, int paymentId)
    {
        var deletePaymentCommand = new DeletePaymentCommand{OrderId = orderId, PaymentId = paymentId};
        var result = await _mediator.Send(deletePaymentCommand);

        if(!result) return NotFound();
        
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdatePayment(int orderId, UpdatePaymentCommand updatePaymentCommand)
    {
        if(orderId != updatePaymentCommand.OrderId) return BadRequest();

        var updatePaymentCommandResponse = await _mediator.Send(updatePaymentCommand);

        if(!updatePaymentCommandResponse.IsSuccess){
            return RequestValidationProblem(updatePaymentCommandResponse, ModelState);
        }
        return NoContent();

    }

}