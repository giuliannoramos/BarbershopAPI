namespace Barbearia.Domain.Entities;

public class StockHistorySupplier : StockHistory
{
    public int PersonId {get;set;}
    public Person? Supplier {get;set;}

    
}