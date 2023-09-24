using System.Dynamic;
using System.Security;

namespace Barbearia.Domain.Entities;

public class Payment
{
    public int PaymentId { get; set; }
    public DateTime BuyDate { get; set; } 
    public Decimal GrossTotal { get; set; }
    public string PaymentMethod { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty;
    public int Status { get; set; }
    public decimal NetTotal { get; set; }
    public int? CouponId { get; set; }
    public Coupon? Coupon { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; } 


    void IsDataValid()
    {
        try
        {
            DateTime dataParseada = BuyDate;
        }
        catch (FormatException)
        {
            throw new Exception("A data deve ser válida");
        }
    }

    void IsGrossTotaValid()
    {
        if(GrossTotal<0)throw new Exception("O grossTotal deve ser maior que 0");
    }

    void IsPaymentValid()
    {
        if(PaymentMethod != "Débito" || PaymentMethod != "Crédito" || PaymentMethod != "Dinheiro")
        {
            throw new Exception("Forma de pagamento não suportada");
        }
        
    }

    void IsNetTotalValid()
    {
        if(NetTotal<0) throw new Exception("O Net Total deve ser maior que 0");
    }

    void IsOrderValid()
    {
        if(Order == null) throw new Exception("O pagamento deve ter uma order");
    }

    bool IsValid()
    {
        IsDataValid();

        IsGrossTotaValid();

        IsPaymentValid();

        IsNetTotalValid();

        IsOrderValid();

        return true;
    }

}

