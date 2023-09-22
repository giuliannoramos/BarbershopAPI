using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;

namespace Barbearia.Application.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, CreateAddressCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateAddressCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CreateAddressCommandResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        CreateAddressCommandResponse response = new();

        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.PersonId);
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

        _customerRepository.AddAddress(customerFromDatabase, addressEntity);
        await _customerRepository.SaveChangesAsync();

        response.Address = _mapper.Map<CreateAddressDto>(addressEntity);
        return response;
    }
}