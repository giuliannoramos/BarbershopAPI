using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using FluentValidation;
using MediatR;

namespace Barbearia.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateCustomerCommand> _validator;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<UpdateCustomerCommand> validator)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        UpdateCustomerCommandResponse response = new();

        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.ToDictionary())
            {
                response.Errors.Add(error.Key, error.Value);
            }

            return response;
        }

        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.PersonId);

        if (customerFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Customer not found." });
            return response;
        }

        _mapper.Map(request, customerFromDatabase);

        await _customerRepository.SaveChangesAsync();

        return response;
    }
}