namespace Barbearia.Domain.Entities;

public class Coupon
{
    public int CouponId{get; set;}
    public string CouponCode {get; set;} = string.Empty;
    public int DiscountPercent{get; set;}
    public DateTime CreationDate {get; set;}
    public DateTime ExpirationDate{get; set;}
    public List<Payment> Payments{get; set;} = new();

}