using FluentValidation;

namespace Barbearia.Application.Features.Telephones.Commands.CreateTelephone;
public class CreateTelephoneCommandValidator : AbstractValidator<CreateTelephoneCommand>
{
    public CreateTelephoneCommandValidator()
    {
        RuleFor(t => t.Number)
               .NotEmpty()
                    .WithMessage("Telephone number cannot be empty")
                .MaximumLength(80)
                    .WithMessage("Telephone number should have at most 80 characters")
                .Must(CheckNumber)
                    .WithMessage("Número de telefone inválido. Use o formato: 47988887777.");

        RuleFor(t => t.Type)
                .NotEmpty()
                    .WithMessage("Telephone type is required")
                .IsInEnum()
                    .WithMessage("Tipo de telefone inválido. O tipo deve ser Móvel ou Fixo.");
    }

    public bool CheckNumber(string number)
    {
        if (!(number.Length == 11 && number.All(char.IsDigit)))
        {
            return false;
        }
        return true;
    }
}