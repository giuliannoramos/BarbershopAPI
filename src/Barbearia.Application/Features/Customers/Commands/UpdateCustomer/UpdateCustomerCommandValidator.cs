using FluentValidation;

namespace Barbearia.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty()
                          .WithMessage("You shoul fill the Id");
        RuleFor(c => c.Name).NotEmpty()
                           .WithMessage("You shoul fill the Name")
                           .MaximumLength(100)
                           .WithMessage("The name shouldn't have more than 100 characters.");
        RuleFor(c => c.BirthDate).NotEmpty()
                                 .WithMessage("You shoul fill the BirthDate");
        RuleFor(c => c.Gender).NotEmpty()
                              .WithMessage("You shoul fill the Gender");
        RuleFor(c => c.Cpf).NotEmpty()
                           .WithMessage("You shoul fill the Cpf")
                           .Must(ValidateCPF)
                        //    .When(c => c.Cpf != null, ApplyConditionTo.CurrentValidator)
                           .WithMessage("The CPf should be a valid number");
        RuleFor(c => c.Email).NotEmpty()
                             .WithMessage("You shoul fill the Email");//botar mais pra frente um validador de email
    }

     private bool ValidateCPF(string cpf)
    {
        // Remove non-numeric characters
        cpf = cpf.Replace(".", "").Replace("-", "");

        // Check if it has 11 digits
        if (cpf.Length != 11)
        {
            return false;
        }

        // Check if all digits are the same
        bool allDigitsEqual = true;
        for (int i = 1; i < cpf.Length; i++)
        {
            if (cpf[i] != cpf[0])
            {
                allDigitsEqual = false;
                break;
            }
        }
        if (allDigitsEqual)
        {
            return false;
        }

        // Check first verification digit
        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += int.Parse(cpf[i].ToString()) * (10 - i);
        }
        int remainder = sum % 11;
        int verificationDigit1 = remainder < 2 ? 0 : 11 - remainder;
        if (int.Parse(cpf[9].ToString()) != verificationDigit1)
        {
            return false;
        }

        // Check second verification digit
        sum = 0;
        for (int i = 0; i < 10; i++)
        {
            sum += int.Parse(cpf[i].ToString()) * (11 - i);
        }
        remainder = sum % 11;
        int verificationDigit2 = remainder < 2 ? 0 : 11 - remainder;
        if (int.Parse(cpf[10].ToString()) != verificationDigit2)
        {
            return false;
        }

        return true;
    }
}