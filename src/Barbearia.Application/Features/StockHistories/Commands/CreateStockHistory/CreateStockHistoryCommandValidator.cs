using FluentValidation;

namespace Barbearia.Application.Features.StockHistories.Commands.CreateStockHistory;

public class CreateStockHistoryCommandValidator : AbstractValidator<CreateStockHistoryCommand>
{
    public CreateStockHistoryCommandValidator()
    {
        RuleFor(o=>o.Operation)
            .NotEmpty()
                .WithMessage("You should fill out an operation");
        
        RuleFor(o=>o.CurrentPrice)
            .NotEmpty()
                .WithMessage("Current price can not be empty")
            .LessThanOrEqualTo(999.99m)
                .WithMessage("Current pricemust be less than 1000.00");

        RuleFor(o=>o.Amount)
            .NotEmpty()
                .WithMessage("You should fill out an amount");

        RuleFor(o=>o.Timestamp)
            .NotEmpty()
                .WithMessage("Timestamp cannot be empty");

        RuleFor(o=>o.LastStockQuantity)
            .NotEmpty()
                .WithMessage("Last stock quantity cannot be empty");

        RuleFor(o=>o.ProductId)
            .NotEmpty()
                .WithMessage("Product id cannot be empty");
    }
}