using System.Data;
using FluentValidation;

namespace Barbearia.Application.Features.Services.Commands.UpdateService;

public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(s => s.ItemId)
            .NotEmpty()
                .WithMessage("You should fill out a Item id");

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