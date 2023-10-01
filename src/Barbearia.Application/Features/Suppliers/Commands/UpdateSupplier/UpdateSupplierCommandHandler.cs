using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features.Customers.Commands.CreateCustomer;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Suppliers.Commands.UpdateSupplier;

public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, UpdateSupplierCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSupplierCommandHandler> _logger;

    public UpdateSupplierCommandHandler(ICustomerRepository customerRepository, IMapper mapper, ILogger<UpdateSupplierCommandHandler> logger)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateSupplierCommandResponse> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        UpdateSupplierCommandResponse response = new();

        var supplierFromDatabase = await _customerRepository.GetSupplierByIdAsync(request.PersonId);

        if (supplierFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Supplier not found." });
            return response;
        }

        var validator = new UpdateSupplierCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, supplierFromDatabase);

        // Validação do cliente
        try
        {
            supplierFromDatabase.ValidateSupplier();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Supplier_Validation", new[] { "Error in supplier validation" });
            _logger.LogError(ex, "erro de validação em update supplier");
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

        await _customerRepository.SaveChangesAsync();

        return response;
    }
}