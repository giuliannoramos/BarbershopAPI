using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Barbearia.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler: IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCustomerCommand> _validator;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<CreateCustomerCommand> validator)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _validator = validator;
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

        customerEntity.IsValid();

        _customerRepository.AddCustomer(customerEntity);
        await _customerRepository.SaveChangesAsync();

        response.Customer = _mapper.Map<CreateCustomerDto>(customerEntity);
        return response;
    }
}