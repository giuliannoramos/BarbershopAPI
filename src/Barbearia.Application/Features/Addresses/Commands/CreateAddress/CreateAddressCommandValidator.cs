using FluentValidation;

namespace Barbearia.Application.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(a => a.Street)
            .NotEmpty()
                .WithMessage("Street cannot be empty")
            .MaximumLength(80)
                .WithMessage("Street should have at most 80 characters");

        RuleFor(a => a.Number)
            .NotEmpty()
                .WithMessage("Number cannot be empty");

        RuleFor(a => a.District)
            .MaximumLength(60)
                .WithMessage("District should have at most 60 characters");

        RuleFor(a => a.City)
            .MaximumLength(60)
                .WithMessage("City should have at most 60 characters");

        RuleFor(a => a.State)
            .MaximumLength(2)
                .WithMessage("State should have at most 2 characters");

        RuleFor(a => a.Cep)
            .Must(ValidateCEP)
                .WithMessage("CEP is not valid");

        RuleFor(a => a.Complement)
            .MaximumLength(80)
                .WithMessage("Complement should have at most 80 characters");
    }

    private bool ValidateCEP(string cep)
    {
        // Remove caracteres não numéricos
        cep = new string(cep.Where(char.IsDigit).ToArray());

        // Verifica se o CEP tem 8 dígitos
        if (cep.Length != 8)
        {
            return false;
        }

        return true;
    }
}