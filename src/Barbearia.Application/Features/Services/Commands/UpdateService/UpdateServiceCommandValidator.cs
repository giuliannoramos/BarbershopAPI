using System.Data;
using FluentValidation;

namespace Barbearia.Application.Features.Services.Commands.UpdateService;

public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(s => s.ServiceCategoryId)
            .NotEmpty()
                .WithMessage("You should fill out a Service category")
            .GreaterThan(0)
                .WithMessage("The id must be positive");

        RuleFor(s => s.DurationMinutes)
            .NotEmpty()
                .WithMessage("You should fill out a Duration minutes")
            .GreaterThan(0)
                .WithMessage("Duration must be greater than 0");        

        RuleFor(s => s.Price)
            .NotEmpty()
                .WithMessage("You should fill out a Price")
            .Must(CheckPrice)
                .WithMessage("Price must be zero or more");

        RuleFor(s => s.Description)
            .NotEmpty()
                .WithMessage("You should fill out a Description");

        RuleFor(s => s.Name)
            .NotEmpty()
                .WithMessage("You should fill out a Name");
    }

    private bool CheckPrice(Decimal Price)
    {
        if (Price <= 0)
        {
            return false;
        }
        return true;
    }
}