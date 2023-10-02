using System.Data;
using FluentValidation;

namespace Barbearia.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p=>p.Name)
            .NotEmpty()
                .WithMessage("You should fill out a name")
            .MaximumLength(50)
                .WithMessage("The name can only have 50 characters at most");
        
        RuleFor(p=> p.Price)
            .NotEmpty()
                .WithMessage("You should fill out a price");
        
        RuleFor(p => p.Description)
            .NotEmpty()
                .WithMessage("You should fill out a description")
            .MaximumLength(200)
                .WithMessage("The description can only have 200 characters at most");

        RuleFor(p=>p.Brand)
            .NotEmpty()
                .WithMessage("You should fill out a brand")
            .MaximumLength(50)
                .WithMessage("The brand can only have 50 characters at most");
        
        RuleFor(p=>p.Status)
            .NotEmpty()
                .WithMessage("Status cannot be empty");

        RuleFor(p=> p.SKU)
            .NotEmpty()
                .WithMessage("SKU cannot be empty");

        RuleFor(p=>p.QuantityInStock)
            .NotEmpty()
                .WithMessage("You should fill out a quantity in stock");

        RuleFor(p=>p.ProductCategoryId)
            .NotEmpty()
                .WithMessage("You should fill out a product category");

        RuleFor(p=>p.QuantityReserved)
            .NotEmpty()
                .WithMessage("Quantity reserved cannot be empty");
    }
}