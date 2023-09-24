namespace Barbearia.Domain.Entities;

public class Coupon
{
    public int CouponId { get; set; }
    public string CouponCode { get; set; } = string.Empty;
    public int DiscountPercent { get; set; }//entre 1 e 100
    public DateTime CreationDate { get; set; }//creation date tem que ser antes de expiration
    public DateTime ExpirationDate { get; set; }
    public List<Payment> Payments { get; set; } = new();


    // bool IsCouponCodeValid()
    // {
    //     //precisa do repository para checar se nao tem repetido

    //     return true;
    // }

    void IsDiscountPercentValid()
    {
        if (!(DiscountPercent > 0 && DiscountPercent < 100))
        {
            throw new Exception("A taxa de desconto tem de estar entre 1 e 100 ");
        }
    }

    void IsExpirationDateValid()
    {
        try
        {
            DateTime dataParseada = ExpirationDate;
        }
        catch (FormatException)
        {
            throw new Exception("A expiration date deve ser válida");
        }

        if (CreationDate > ExpirationDate) throw new Exception("A data de expiração não pode ser antes da criação");
    }

    void IsCreationDateValid()
    {
        try
        {
            DateTime dataParseada = ExpirationDate;
        }
        catch (FormatException)
        {
            throw new Exception("A creation date deve ser válida");
        }
    }

    bool IsValid()
    {
        // if(!IsCouponCodeValid()) return false;

        IsCreationDateValid();

        IsExpirationDateValid();

        IsDiscountPercentValid();

        return true;
    }
}