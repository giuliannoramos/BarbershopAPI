using System.Data;
using FluentValidation;

namespace Barbearia.Application.Features.Services.Commands.CreateService;

public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {

        RuleFor(s => s.ServiceCategoryId)
            .NotEmpty()
                .WithMessage("You should fill out a Service category");

        RuleFor(s => s.DurationMinutes)
            .NotEmpty()
                .WithMessage("You should fill out a Duration minutes");        

        RuleFor(s => s.Price)
            .NotEmpty()
                .WithMessage("You should fill out a Price");

        RuleFor(s => s.Description)
            .NotEmpty()
                .WithMessage("You should fill out a Description");

        RuleFor(s => s.Name)
            .NotEmpty()
                .WithMessage("You should fill out a Name");
    }
}