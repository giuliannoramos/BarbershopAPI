using System.Data;
using FluentValidation;

namespace Barbearia.Application.Features.Schedules.Commands.UpdateSchedule;

public class UpdateScheduleCommandValidator : AbstractValidator<UpdateScheduleCommand>
{
    public UpdateScheduleCommandValidator()
    {
        RuleFor(s=>s.WorkingDayId)
            .NotEmpty()
                .WithMessage("You should fill out a working day");
        
        RuleFor(s=>s.Status)
            .NotEmpty()
                .WithMessage("You should fill out a status");
    }
}