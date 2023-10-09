using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateEmployeeCommandHandler> _logger;



    public CreateEmployeeCommandHandler(IPersonRepository personRepository, IMapper mapper, ILogger<CreateEmployeeCommandHandler> logger)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        CreateEmployeeCommandResponse response = new();

        var validator = new CreateEmployeeCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var employeeEntity = _mapper.Map<Employee>(request);

        // Validação do funcionario
        try
        {
            employeeEntity.ValidateEmployee();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
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
                response.ErrorType = Error.InternalServerErrorProblem;
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
                response.ErrorType = Error.InternalServerErrorProblem;
                response.Errors.Add("Address_Validation", new[] { "Error in address validation" });
                _logger.LogError(ex, "erro de validação em create address");
                return response;
            }
        }

        _personRepository.AddEmployee(employeeEntity);
        await _personRepository.SaveChangesAsync();

        response.Employee = _mapper.Map<CreateEmployeeDto>(employeeEntity);
        return response;
    }
}