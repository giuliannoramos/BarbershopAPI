namespace Barbearia.Domain.Entities;

public class Order
{
    public int OrderId{get; set;}
    public int Number {get;set;}
    public int Status{get;set;}
    public DateTime BuyDate{get;set;}
    public int PersonId { get; set; }
    public Customer? customer { get; set; }

    public Payment? payment{get; set; }

    //no banco, order está com a id de payment, mas não é necessário,
    //pois payment já tem o id de order. Além disso, a relação é opcional.

}