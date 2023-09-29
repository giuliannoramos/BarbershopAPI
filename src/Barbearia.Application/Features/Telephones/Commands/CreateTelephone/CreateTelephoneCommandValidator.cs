using FluentValidation;

namespace Barbearia.Application.Features.Telephones.Commands.CreateTelephone;
 public class CreateTelephoneCommandValidator : AbstractValidator<CreateTelephoneCommand>
 {
    public CreateTelephoneCommandValidator(){
        RuleFor(t => t.Number)
            .Must(ValidateNumber)
                .WithMessage("The number should be 11 characters");

        RuleFor(t => t.Type)
            .NotEmpty()
                .WithMessage("You should fill out a number type");
    }
    private bool ValidateNumber(string number)
    {
        number = new string(number.Where(char.IsDigit).ToArray());

        if (number.Length != 11){
            return false;
        }

        return true;
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