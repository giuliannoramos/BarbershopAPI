using Barbearia.Application.Features.Addresses.Commands.CreateAddress;
using Barbearia.Application.Features.Addresses.Queries.GetAddress;
using Barbearia.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;


namespace Barbearia.Api.Controllers;

[Route("api/customers/{customerId}/addresses")]
public class AddressesController : MainController
{
    private readonly IMediator _mediator;
    public AddressesController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet(Name = "GetAddress")]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddress(int customerId)
    {
        var getAddressQuery = new GetAddressQuery { PersonId = customerId };
        var addressToReturn = await _mediator.Send(getAddressQuery);

        if (addressToReturn == null) return NotFound();

        return Ok(addressToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateAddressCommandResponse>> CreateAddress(int customerId, [FromBody] CreateAddressCommand createAddressCommand)
    {
        if (customerId != createAddressCommand.PersonId) return BadRequest();

        var createAddressCommandResponse = await _mediator.Send(createAddressCommand);

        if (!createAddressCommandResponse.IsSuccess)
            return RequestValidationProblem(createAddressCommandResponse, ModelState);

        var addressForReturn = createAddressCommandResponse.Address;

        return CreatedAtRoute
        (
            "GetAddress",
            new
            {
                customerId,
                addressId = addressForReturn.AddressId
            },
            addressForReturn
        );
    }
}