using System.Data;
using FluentValidation;

namespace Barbearia.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(o=>o.Number)
            .NotEmpty()
                .WithMessage("You should fill out a number");
        
        RuleFor(o=>o.Status)
            .NotEmpty()
                .WithMessage("Status cannot be empty");

        RuleFor(o=>o.PersonId)
            .NotEmpty()
                .WithMessage("You should fill out a person");

        RuleFor(o=>o.BuyDate)
            .NotEmpty()
                .WithMessage("BuyDate cannot be empty");
    }
}