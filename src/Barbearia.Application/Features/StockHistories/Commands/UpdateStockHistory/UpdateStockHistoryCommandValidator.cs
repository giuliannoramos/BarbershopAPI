using FluentValidation;

namespace Barbearia.Application.Features.StockHistories.Commands.UpdateStockHistory;

public class UpdateStockHistoryCommandValidator : AbstractValidator<UpdateStockHistoryCommand>
{
    public UpdateStockHistoryCommandValidator()
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

        // RuleFor(o=>o.PersonId)
        //     .NotEmpty()
        //         .WithMessage("Person id cannot be empty");

        RuleFor(o=>o.ProductId)
            .NotEmpty()
                .WithMessage("Product id cannot be empty");

        // RuleFor(o=>o.OrderId)
        //     .NotEmpty()
        //         .WithMessage("Order id cannot be empty");
    }

    public bool IsTimestampValid(DateTime timestamp)
    {
        if (timestamp.Kind == DateTimeKind.Utc)
        {
            return true; //Exemplo valido no banco -> "BuyDate": "2023-09-22T10:00:00Z"
        }

        return false;
    }
}