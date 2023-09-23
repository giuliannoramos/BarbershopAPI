using FluentValidation;

namespace Barbearia.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.PersonId)
                .NotEmpty()
                .WithMessage("PersonId cannot be empty");

            RuleFor(o => o.Number)
                .NotEmpty()
                .WithMessage("Number cannot be empty");

            RuleFor(o => o.Status)
                .NotEmpty()
                .WithMessage("Status cannot be empty");

            RuleFor(o => o.BuyDate)
                .NotEmpty()
                .WithMessage("BuyDate cannot be empty")
                .Must(IsDateTimeValid)
                .WithMessage("BuyDate must be a valid DateTime");
        }

        public bool IsDateTimeValid(DateTime buyDate)
        {
            if (buyDate.Kind == DateTimeKind.Utc)
            {
                return true; //Exemplo valido no banco -> "BuyDate": "2023-09-22T10:00:00Z"
            }

            return false;
        }
    }
}