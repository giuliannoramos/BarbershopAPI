using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Telephones.Commands.CreateTelephone;

public class CreateTelephoneCommandHandler : IRequestHandler<CreateTelephoneCommand, CreateTelephoneCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateTelephoneCommandHandler> _logger;
    public CreateTelephoneCommandHandler(ICustomerRepository customerRepository, IMapper mapper, ILogger<CreateTelephoneCommandHandler> logger)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
        _logger = logger;
    }
    public async Task<CreateTelephoneCommandResponse> Handle(CreateTelephoneCommand request, CancellationToken cancellationToken)
    {
        CreateTelephoneCommandResponse response = new();

        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.PersonId);
        if (customerFromDatabase == null)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("PersonId", new[] { "Customer Not found in database" });
            return response;
        }
        if (customerFromDatabase.Telephones.Any())
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Telephone", new[] { "Only one telephone per customer" });
            return response;
        }

        var validator = new CreateTelephoneCommandValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Telephone", new[] { "Telephone number is not valid" });
            return response;
        }

        var telephoneEntity = _mapper.Map<Telephone>(request);

        try
        {
            telephoneEntity.ValidateTelephone();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Telephone_Validation", new[] { "Error in telephone validation" });
            _logger.LogError(ex, "erro de validação em create telephone");
            return response;
        }

        _customerRepository.AddTelephone(customerFromDatabase, telephoneEntity);
        await _customerRepository.SaveChangesAsync();

        response.Telephone = _mapper.Map<CreateTelephoneDto>(telephoneEntity);
        return response;
    }
}