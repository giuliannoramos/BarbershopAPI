using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Features.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(a => a.ScheduleId)
                .NotEmpty()
                    .WithMessage("Should have a schedule");
            RuleFor(a => a.CustomerId)
                .NotEmpty()
                    .WithMessage("Should have a customer");
            RuleFor(a => a.StartDate)
                .NotEmpty()
                    .WithMessage("start date cannot be empty");
            RuleFor(a => a.FinishDate)
                .NotEmpty()
                    .WithMessage("finish date cannot be empty");
            RuleFor(a => a.Status)
                .NotEmpty()
                    .WithMessage("status cannot be empty");
            RuleFor(a => a.StartServiceDate)
                .NotEmpty()
                    .WithMessage("start service date cannot be empty");
            RuleFor(a => a.FinishServiceDate)
                .NotEmpty()
                    .WithMessage("finish service date cannot be empty");
            RuleFor(a => a.CancellationDate)
                .NotEmpty()
                    .WithMessage("cancellation date cannot be empty");
            RuleFor(a => a.ConfirmedDate)
                .NotEmpty()
                    .WithMessage("confirmed date cannot be empty");
            RuleFor(a => a.ServicesId)
                .NotEmpty()
                    .WithMessage("Should have at least one service");
        }
    }
}
