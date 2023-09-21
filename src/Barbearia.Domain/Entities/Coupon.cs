namespace Barbearia.Domain.Entities;

public class Coupon
{
    public int CouponId{get; set;}
    public string CouponCode {get; set;} = string.Empty;
    public int DiscountPercent{get; set;}//entre 1 e 100
    public DateTime CreationDate {get; set;}//creation date tem que ser antes de expiration
    public DateTime ExpirationDate{get; set;}
    public List<Payment> Payments{get; set;} = new();
    

    bool IsCouponCodeValid()
    {
        //precisa do repository para checar se nao tem repetido

        return true;
    }

    bool IsDiscountPercentValid()
    {
        if(!(DiscountPercent > 0 && DiscountPercent <100)) return false;

        return true;
    }

    bool IsCreationDateValid()
    {
        if(CreationDate>ExpirationDate) return false;
    
        return true;
    }
}