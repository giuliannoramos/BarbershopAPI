using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCustomerCommandHandler> _logger;



    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, ILogger<CreateCustomerCommandHandler> logger)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        CreateCustomerCommandResponse response = new();

        var validator = new CreateCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }



        var customerEntity = _mapper.Map<Customer>(request);

        try
        {
            customerEntity.IsValid();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            _logger.LogError(ex, "erro de validação em create customer");
            return response;
        }



        _customerRepository.AddCustomer(customerEntity);
        await _customerRepository.SaveChangesAsync();

        response.Customer = _mapper.Map<CreateCustomerDto>(customerEntity);
        return response;
    }
}