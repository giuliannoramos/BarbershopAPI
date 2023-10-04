using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Schedules.Commands.CreateSchedule;

public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, CreateScheduleCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IPersonRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateScheduleCommandHandler> _logger;

    public CreateScheduleCommandHandler(IPersonRepository personRepository, IPersonRepository customerRepository, IMapper mapper, ILogger<CreateScheduleCommandHandler> logger)
    {
        _personRepository = personRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateScheduleCommandResponse> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        CreateScheduleCommandResponse response = new();

        var validator = new CreateScheduleCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if(!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var ScheduleEntity = _mapper.Map<Schedule>(request);

        try
        {
            ScheduleEntity.ValidateSchedule();
        }
        catch(Exception ex)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Schedule_Validation", new[] {"Error in Schedule validation"});
            _logger.LogError(ex,"erro de validação em create Schedule");
            return response;
        }

        _personRepository.AddSchedule(ScheduleEntity);
        await _personRepository.SaveChangesAsync();

        response.Schedule = _mapper.Map<CreateScheduleDto>(ScheduleEntity);
        return response;
    }
}