using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, CreateAddressCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateAddressCommandHandler> _logger;


    public CreateAddressCommandHandler(IPersonRepository personRepository, IMapper mapper, ILogger<CreateAddressCommandHandler> logger)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateAddressCommandResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        CreateAddressCommandResponse response = new();

        var customerFromDatabase = await _personRepository.GetCustomerByIdAsync(request.PersonId);
        if (customerFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Customer not found in the database." });
            return response;
        }

        if (customerFromDatabase.Addresses.Any())
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Addresses", new[] { "Only one address is allowed." });
            return response;
        }

        var validator = new CreateAddressCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var addressEntity = _mapper.Map<Address>(request);

        try
        {
            addressEntity.ValidateAddress();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Address_Validation", new[] { "Error in address validation" });
            _logger.LogError(ex, "erro de validação em create address");
            return response;
        }

        _personRepository.AddAddress(customerFromDatabase, addressEntity);
        await _personRepository.SaveChangesAsync();

        response.Address = _mapper.Map<CreateAddressDto>(addressEntity);
        return response;
    }
}