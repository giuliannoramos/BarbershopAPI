using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features.Customers.Commands.CreateCustomer;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, ILogger<UpdateCustomerCommandHandler> logger)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        UpdateCustomerCommandResponse response = new();

        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.PersonId);

        if (customerFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Customer not found." });
            return response;
        }

        var validator = new UpdateCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, customerFromDatabase);

        try
        {
            customerFromDatabase.IsValid();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Customer_Validation", new[] { "Error in customer validation" });
            _logger.LogError(ex, "erro de validação em update customer");
            return response;
        }
        await _customerRepository.SaveChangesAsync();

        return response;
    }
}