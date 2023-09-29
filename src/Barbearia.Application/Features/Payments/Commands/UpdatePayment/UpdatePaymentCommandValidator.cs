using FluentValidation;

namespace Barbearia.Application.Features.Payments.Commands.UpdatePayment;

 public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
 {
    public UpdatePaymentCommandValidator(){

        // RuleFor(p => p.GrossTotal)
        //     .NotEmpty()
        //         .WithMessage("The gross total cannot be empty");

        //acho que o sistema deva preencher isso sozinho, assim como o net total

        RuleFor(p => p.PaymentMethod)
            .NotEmpty()
                .WithMessage("The payment method cannot be empty")
            .Must(IsPaymentValid)
                .WithMessage("The payment must be 'Crédito', 'Débito' ou 'Dinheiro'");

        RuleFor(p => p.Description)
            .MaximumLength(200)
                .WithMessage("The description can only have 200 characters at most");

        RuleFor(p => p.Status)
            .NotEmpty()
                .WithMessage("The status cannot be empty");

        // RuleFor(p => p.NetTotal)
        //     .NotEmpty()
        //         .WithMessage("The net total cannot be empty");

        // RuleFor(p => p.CouponId)
        //     .NotEmpty()
        //         .WithMessage("The buy date cannot be empty");

        //coupon é opcional

    }

    bool IsPaymentValid(string paymentMethod)
    {

        if(paymentMethod != "Débito" && paymentMethod != "Crédito" && paymentMethod != "Dinheiro")
        {
            return false;
        }

        return true;
        
    }

 }