namespace Barbearia.Domain.Entities;

public class StockHistoryOrder : StockHistory
{
    public int OrderId { get; set; }
    public Order? Order { get; set; }

    private void CheckOrderId()
    {
        if (OrderId <= 0)
        {
            throw new ArgumentException("OrderId deve ser maior que zero.");
        }
    }

    public void ValidateStockHistoryOrder()
    {
        CheckOrderId();
    }
}