using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Barbearia.Application.Features.Telephones.Commands.UpdateTelephone;

public class UpdateTelephoneCommandHandler : IRequestHandler<UpdateTelephoneCommand, UpdateTelephoneCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateTelephoneCommand> _validator;
    public UpdateTelephoneCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<UpdateTelephoneCommand> validator){
        _mapper = mapper;
        _customerRepository = customerRepository;
        _validator = validator;
    }
    public async Task<UpdateTelephoneCommandResponse> Handle(UpdateTelephoneCommand request, CancellationToken cancellationToken)
    {
        UpdateTelephoneCommandResponse response = new();

        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.PersonId);
        if(customerFromDatabase == null){
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("PersonId", new[]{"Customer not found in database"});
            return response;
        }

        var telephoneToUpdate = customerFromDatabase.Telephones.FirstOrDefault(c => c.TelephoneId == request.TelephoneId);
        if(telephoneToUpdate == null){
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Telephone", new[]{"Telephone not found in database"});
            return response;
        }

        var validator = new UpdateTelephoneCommandValidator();
        var validationResult = validator.Validate(request);
        if(!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Telephone", new[]{"Telephone number is not valid"});
            return response;
        }

        _mapper.Map(request, telephoneToUpdate);

        await _customerRepository.SaveChangesAsync();

        return response;
    }
}