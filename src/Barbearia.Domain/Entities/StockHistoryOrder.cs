namespace Barbearia.Domain.Entities;

public class StockHistoryOrder : StockHistory
{
    public int OrderId {get;set;}
    public Order? Order {get;set;}

    
}