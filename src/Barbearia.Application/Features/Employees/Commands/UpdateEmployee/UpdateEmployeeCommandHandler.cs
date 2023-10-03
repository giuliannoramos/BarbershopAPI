using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features.Employees.Commands.CreateEmployee;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateEmployeeCommandHandler> _logger;

    public UpdateEmployeeCommandHandler(IPersonRepository personRepository, IMapper mapper, ILogger<UpdateEmployeeCommandHandler> logger)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        UpdateEmployeeCommandResponse response = new();

        var employeeFromDatabase = await _personRepository.GetEmployeeByIdAsync(request.PersonId);

        if (employeeFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Employee not found." });
            return response;
        }

        var validator = new UpdateEmployeeCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, employeeFromDatabase);

        // Validação do funcionario
        try
        {
            employeeFromDatabase.ValidateEmployee();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Employee_Validation", new[] { "Error in Employee validation" });
            _logger.LogError(ex, "erro de validação em create Employee");
            return response;
        }

        // Validação do número de telefone
        foreach (var telephone in request.Telephones)
        {
            var telephoneEntity = _mapper.Map<Telephone>(telephone);
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
        }

        // Validação do endereço
        foreach (var address in request.Addresses)
        {
            var addressEntity = _mapper.Map<Address>(address);
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
        }

        await _personRepository.SaveChangesAsync();

        return response;
    }
}