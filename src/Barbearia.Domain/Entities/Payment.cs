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


    bool IsDataValid()
    {
        try
        {
            DateTime dataParseada = BuyDate;
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    bool IsGrossTotaValid()
    {
        if(GrossTotal<0)return false;

        return true;
    }

    bool IsPaymentValid()
    {
        if(PaymentMethod != "Débito" || PaymentMethod != "Crédito" || PaymentMethod != "Dinheiro") return false;
        
        return true;
    }

    bool IsNetTotalValid()
    {
        if(NetTotal<0)return false;

        return true;
    }

    bool IsOrderValid()
    {
        if(Order == null) return false;
        return true;
    }

    bool IsValid()
    {
        if(!IsDataValid())return false;

        if(!IsGrossTotaValid())return false;

        if(!IsPaymentValid())return false;

        if(!IsNetTotalValid())return false;

        if(!IsOrderValid())return false;

        return true;
    }

}

