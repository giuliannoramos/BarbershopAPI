using Barbearia.Application.Features.Telephones.Commands.DeleteTelephone;
using Barbearia.Application.Features.Telephones.Query.GetTelephone;
using Barbearia.Application.Features.Telephones.Commands.CreateTelephone;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Barbearia.Application.Features.Telephones.Commands.UpdateTelephone;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/customers/{customerId}/telephones")]
public class TelephoneController : MainController
{
    private readonly IMediator _mediator;

    public TelephoneController(IMediator mediator){
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet(Name = "GetTelephone")]
    public async Task<ActionResult<IEnumerable<TelephoneDto>>> GetTelephone(int customerId)
    {
        var getTelephoneQuery = new GetTelephoneQuery{PersonId = customerId};
        var telephoneToReturn = await _mediator.Send(getTelephoneQuery);

        if(!telephoneToReturn.Any()) return NotFound();

        return Ok(telephoneToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateTelephoneCommandResponse>> CreateTelephone(int customerId, CreateTelephoneCommand createTelephoneCommand)
    {
        if(customerId != createTelephoneCommand.PersonId) return BadRequest();

        var createTelephoneCommandResponse = await _mediator.Send(createTelephoneCommand); 

        if(!createTelephoneCommandResponse.IsSuccess){
            return RequestValidationProblem(createTelephoneCommandResponse, ModelState);
        }

        var telephoneToReturn = createTelephoneCommandResponse.Telephone;

        return CreatedAtRoute(
            "GetTelephone",
            new{
                customerId
            }, telephoneToReturn
            
        );
    }

    [HttpPut]
    public async Task<ActionResult> UpdateTelephone(int customerId, UpdateTelephoneCommand updateTelephoneCommand)
    {
        if(customerId != updateTelephoneCommand.PersonId) return BadRequest();

        var updateAddressCommandResponse = await _mediator.Send(updateTelephoneCommand);

        if(!updateAddressCommandResponse.IsSuccess){
            return RequestValidationProblem(updateAddressCommandResponse, ModelState);
        }
        return NoContent();

    }

    [HttpDelete ("{telephoneId}")]
    public async Task<ActionResult> DeleteTelephone(int customerId, int telephoneId)
    {
        var deleteTelephoneCommand = new DeleteTelephoneCommand{PersonId = customerId, TelefoneId = telephoneId};
        var result = await _mediator.Send(deleteTelephoneCommand);

        if(!result) return NotFound();
        
        return NoContent();
    }
}