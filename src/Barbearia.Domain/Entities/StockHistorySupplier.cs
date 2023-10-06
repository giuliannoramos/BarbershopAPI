namespace Barbearia.Domain.Entities;

public class StockHistorySupplier : StockHistory
{
    public int PersonId { get; set; }
    public Person? Supplier { get; set; }

    private void CheckPersonId()
    {
        if (PersonId <= 0)
        {
            throw new ArgumentException("PersonId deve ser maior que zero.");
        }
    }

    public void ValidateStockHistorySupplier()
    {
        CheckPersonId();
    }
}