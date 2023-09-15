using Barbearia.Application.Features.Customers.Commands;
using Barbearia.Application.Features.Customers.Queries.GetAllCustomers;
using Barbearia.Application.Features.Customers.Queries.GetCustomerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[Route("api/customers")]
public class CustomersController : MainController
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("{customerId}", Name = "GetCustomerById")]
    public async Task<ActionResult<GetCustomerByIdDto>> GetCustomerById(int customerId)
    {
        var getCustomerByIdQuery = new GetCustomerByIdQuery { PersonId = customerId };

        var customerToReturn = await _mediator.Send(getCustomerByIdQuery);

        if (customerToReturn == null) return NotFound();

        return Ok(customerToReturn);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllCustomersDto>>> GetCustomers()
    {
        var getAllCustomersQuery = new GetAllCustomersQuery { }; 

        var customersToReturn = await _mediator.Send(getAllCustomersQuery); 

        return Ok(customersToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateCustomerCommandResponse>> CreateCustomer([FromBody]CreateCustomerCommand createCustomerCommand)
    {
        var createCustomerCommandResponse = await _mediator.Send(createCustomerCommand);

        if(!createCustomerCommandResponse.IsSuccess)
            return RequestValidationProblem(createCustomerCommandResponse, ModelState);

        var customerForReturn = createCustomerCommandResponse.Customer;

        return CreatedAtRoute
        (
            "GetCustomerById",
            new { customerId = customerForReturn.PersonId },
            customerForReturn
        );
    }
}