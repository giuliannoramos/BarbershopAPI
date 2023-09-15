using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Barbearia.Application.Features.Customers.Commands;

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

        var validationResult = _validator.Validate(request);

        if(!validationResult.IsValid)
        {
            foreach(var error in validationResult.ToDictionary())
            {
                response.Errors.Add(error.Key, error.Value);
            }

            return response;
        }

        var customerEntity = _mapper.Map<Customer>(request);

        _customerRepository.AddCustomer(customerEntity);
        await _customerRepository.SaveChangesAsync();

        response.Customer = _mapper.Map<CreateCustomerDto>(customerEntity);
        return response;
    }
}