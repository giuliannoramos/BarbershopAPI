using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;

namespace Barbearia.Application.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, UpdateAddressCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateAddressCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<UpdateAddressCommandResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        UpdateAddressCommandResponse response = new UpdateAddressCommandResponse();

        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.PersonId);
        if (customerFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Customer not found in the database." });
            return response;
        }

        var addressToUpdate = customerFromDatabase.Addresses.FirstOrDefault(a => a.AddressId == request.AddressId);

        if (addressToUpdate == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("AddressId", new[] { "Address not found for the specified PersonId." });
            return response;
        }

        var validator = new UpdateAddressCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        _mapper.Map(request, addressToUpdate);

        await _customerRepository.SaveChangesAsync();

        return response;
    }
}